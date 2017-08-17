using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public class BinaryExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;

        public BinaryExpressionParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public IClause Parse(Expression expression, ClauseContext context)
        {
            return ParseExact(expression as BinaryExpression, context);
        }

        private IClause ParseExact(BinaryExpression expression, ClauseContext context)
        {
            var left = _lookup.Parse(expression.Left, context);
            var right = _lookup.Lookup(expression.Right).Parse(expression.Right, context);
            var op = _lookup.Parse(expression.NodeType, left, right);

            var clause = new MutableClause(op)
                .WrapParentheses();

            return clause;
        }

        public bool CanParse(Expression expression)
        {
            return expression is BinaryExpression;
        }
    }
}