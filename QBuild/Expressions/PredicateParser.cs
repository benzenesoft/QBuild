using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PredicateParser : IPredicateParser
    {
        private IEnumerable<IExpressionParser> _prioritisedParsers;
        private readonly BinaryExpressionParser _binaryParser;
        private readonly ConstantExpressionParser _constantParser;
        private readonly PropertyExpressionParser _propertyParser;

        public PredicateParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            _constantParser = new ConstantExpressionParser();
            _propertyParser = new PropertyExpressionParser(nameResolver);
            _binaryParser = new BinaryExpressionParser(operatorResolver, _constantParser, _propertyParser);

            _prioritisedParsers = new IExpressionParser[]
            {
                _constantParser, _propertyParser, _binaryParser
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