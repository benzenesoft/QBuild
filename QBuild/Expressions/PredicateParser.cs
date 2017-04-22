using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PredicateParser : IPredicateParser
    {
        private readonly BinaryExpressionParser _binaryParser;
        private readonly ConstantExpressionParser _constantParser;

        public PredicateParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            _constantParser = new ConstantExpressionParser();
            var propertyParser = new PropertyExpressionParser(nameResolver);
            _binaryParser = new BinaryExpressionParser(operatorResolver, _constantParser, propertyParser);
        }

        public ISql Parse<T>(Expression<Func<T, bool>> predicate)
        {
            return ParseExpression(predicate.Body);
        }

        protected ISql ParseExpression(Expression expression)
        {
            if (expression is ConstantExpression)
            {
                return _constantParser.Parse((ConstantExpression) expression);
            }
            if (expression is BinaryExpression)
            {
                return _binaryParser.Parse((BinaryExpression) expression);
            }
            return null;
        }
    }
}