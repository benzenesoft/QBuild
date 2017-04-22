using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PredicateParser : IPredicateParser
    {
        private IEnumerable<IExpressionParser> _prioritisedParsers;

        public PredicateParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            var constantParser = new ConstantExpressionParser();
            var propertyParser = new PropertyExpressionParser(nameResolver);
            var binaryParser = new BinaryExpressionParser(operatorResolver, constantParser, propertyParser);
            var notExpressionParser = new NotExpressionParser(nameResolver);
            _prioritisedParsers = new IExpressionParser[]
            {
                constantParser, propertyParser, binaryParser, notExpressionParser
            };
        }

        public ISql Parse<T>(Expression<Func<T, bool>> predicate)
        {
            return ParseExpression(predicate.Body);
        }

        protected ISql ParseExpression(Expression expression)
        {
            var parser = _prioritisedParsers.FirstOrDefault(p => p.CanParse(expression));

            return parser?.Parse(expression);
        }
    }
}