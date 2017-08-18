using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public class UnaryExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;

        public UnaryExpressionParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public bool CanParse(Expression expression)
        {
            return expression is UnaryExpression;
        }

        IClause IExpressionParser.Parse(Expression expression, ClauseContext context)
        {
            return Parse(expression as UnaryExpression, context);
        }

        public IClause Parse(UnaryExpression expression, ClauseContext context)
        {
            return _lookup.Parse(expression.Operand, context);
        }
    }
}
