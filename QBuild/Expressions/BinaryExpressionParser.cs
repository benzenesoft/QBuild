using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class BinaryExpressionParser : IExpressionParser
    {
        private readonly IOperatorResolver _operatorResolver;
        private readonly ConstantExpressionParser _constantParser;
        private PropertyExpressionParser _propertyParser;

        public BinaryExpressionParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
            : this(operatorResolver, new ConstantExpressionParser(), new PropertyExpressionParser(nameResolver))
        {
        }

        public BinaryExpressionParser(IOperatorResolver operatorResolver, ConstantExpressionParser constantParser
            , PropertyExpressionParser propertyParser)
        {
            _operatorResolver = operatorResolver;
            _constantParser = constantParser;
            _propertyParser = propertyParser;
        }

        public ISql Parse(Expression expression)
        {
            return ParseExact(expression as BinaryExpression);
        }

        private ISql ParseExact(BinaryExpression expression)
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
                return _propertyParser.Parse((MemberExpression)expression);
            }
            if (expression is ConstantExpression)
            {
                return _constantParser.Parse((ConstantExpression)expression);
            }

            throw new ArgumentException("expression must be a property on constant", nameof(expression));
        }

        public bool CanParse(Expression expression)
        {
            var be = expression as BinaryExpression;
            if (be != null)
            {
                return _propertyParser.CanParse(be.Left) && _constantParser.CanParse(be.Right) ||
                       _propertyParser.CanParse(be.Left) && _constantParser.CanParse(be.Right) ||
                       _propertyParser.CanParse(be.Left) && _propertyParser.CanParse(be.Right);
            }

            return false;
        }
    }
}