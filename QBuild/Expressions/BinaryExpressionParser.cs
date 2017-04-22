using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class BinaryExpressionParser : ExpressionParser<BinaryExpression>
    {
        private readonly IOperatorResolver _operatorResolver;
        private readonly ConstantExpressionParser _constantParser;
        private PropertyExpressionParser _propertyParser;

        public BinaryExpressionParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            _operatorResolver = operatorResolver;
            _constantParser = new ConstantExpressionParser();
            _propertyParser = new PropertyExpressionParser(nameResolver);
        }

        protected override ISql ParseImpl(BinaryExpression expression)
        {
            var left = AsColumnOrValue(expression.Left);
            var op = _operatorResolver[expression.NodeType];
            var right = AsColumnOrValue(expression.Right);

            var sql = new Sql().Append(left).Append($" {op} ").Append(right);

            return sql;
        }

        private ISql AsColumnOrValue(Expression expression)
        {
            if (expression is MemberExpression)
            {
                return _propertyParser.Parse(expression);
            }
            if (expression is ConstantExpression)
            {
                return _constantParser.Parse(expression);
            }

            throw new ArgumentException("expression must be a property on constant", nameof(expression));
        }
    }
}