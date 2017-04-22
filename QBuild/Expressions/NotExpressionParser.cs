using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class NotExpressionParser : IExpressionParser
    {
        private PropertyExpressionParser _propertyParser;
        public NotExpressionParser(INameResolver nameResolver)
        {
            _propertyParser = new PropertyExpressionParser(nameResolver);
        }

        public bool CanParse(Expression expression)
        {
            var ue = expression as UnaryExpression;
            return ue != null && ue.NodeType == ExpressionType.Not && _propertyParser.CanParse(ue.Operand);
        }

        public ISql Parse(Expression expression)
        {
            return ParseExact((UnaryExpression) expression);
        }

        public ISql ParseExact(UnaryExpression expression)
        {
            return new Sql().Append("NOT ").Append(_propertyParser.Parse(expression.Operand));
        }
    }
}