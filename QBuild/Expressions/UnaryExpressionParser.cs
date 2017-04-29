using System.Linq.Expressions;
using BenzeneSoft.QBuild.Sqls;

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

        ISql IExpressionParser.Parse(Expression expression)
        {
            return Parse(expression as UnaryExpression);
        }

        public ISql Parse(UnaryExpression expression)
        {
            var inner =  _lookup[expression.Operand];
            return inner.Parse(expression.Operand);
        }
    }
}
