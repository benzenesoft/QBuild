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
            var left = _lookup[expression.Left].Parse(expression.Left, context);
            var op = _lookup[expression.NodeType];
            var right = _lookup[expression.Right].Parse(expression.Right, context);

            var clause = new MutableClause(op.Parse(expression.NodeType, left, right))
                .WrapParentheses();

            return clause;
        }

        public bool CanParse(Expression expression)
        {
            return expression is BinaryExpression;
        }
    }
}