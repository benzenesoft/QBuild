using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using static System.Linq.Expressions.ExpressionType;

namespace BenzeneSoft.QBuild.Expressions
{
    public class NullityExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;

        public NullityExpressionParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public bool CanParse(Expression expression)
        {
            var canParse = EqualOrNotEqual(expression);

            var binExpression = expression as BinaryExpression;
            canParse = canParse && binExpression != null;
            canParse = canParse && HasNullExpression(binExpression);

            return canParse;
        }

        private static bool HasNullExpression(BinaryExpression binExpression)
        {
            var constLeft = binExpression.Left as ConstantExpression;
            var constRight = binExpression.Right as ConstantExpression;

            var canParse = (constLeft ?? constRight) != null;
            canParse = canParse && (constLeft ?? constRight).Value == null;
            return canParse;
        }

        private static bool EqualOrNotEqual(Expression binExpression)
        {
            return binExpression.NodeType == Equal ||
                   binExpression.NodeType == NotEqual;
        }

        public IClause Parse(Expression expression)
        {
            return ParseExact(expression as BinaryExpression);
        }

        private IClause ParseExact(BinaryExpression binaryExpression)
        {
            Expression other;
            
            if (binaryExpression.Left is ConstantExpression)
            {
                other = binaryExpression.Right;
            }
            else
            {
                other = binaryExpression.Left;
            }

            var nullCheck = binaryExpression.NodeType == Equal ? " IS NULL" : " IS NOT NULL";

            var otherSql = _lookup[other].Parse(other);
            var sql = new MutableClause().Append(otherSql).Append(nullCheck).WrapParentheses();

            return sql;
        }
    }
}
