using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PredicateParser : IPredicateParser
    {
        private readonly BinaryExpressionParser _binaryParser;
        private readonly ConstantExpressionParser _constantParser;
        private readonly PropertyExpressionParser _propertyParser;

        public PredicateParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            _constantParser = new ConstantExpressionParser();
            _propertyParser = new PropertyExpressionParser(nameResolver);
            _binaryParser = new BinaryExpressionParser(operatorResolver, _constantParser, _propertyParser);
        }

        public ISql Parse<T>(Expression<Func<T, bool>> predicate)
        {
            return ParseExpression(predicate.Body);
        }

        protected ISql ParseExpression(Expression expression)
        {
            if (expression is ConstantExpression)
                return _constantParser.Parse((ConstantExpression) expression);

            if (expression is MemberExpression)
            {
                var me = (MemberExpression)expression;
                if (me.Member is PropertyInfo)
                    return _propertyParser.Parse(me);
            }

            if (expression is BinaryExpression)
                return _binaryParser.Parse((BinaryExpression) expression);
            return null;
        }
    }
}