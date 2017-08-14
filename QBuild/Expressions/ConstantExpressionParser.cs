using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ConstantExpressionParser : IExpressionParser
    {
        public IClause Parse(Expression expression, ClauseContext context)
        {
            var value = (expression as ConstantExpression).Value;
            if (context == ClauseContext.Select)
            {
                return new Clause(value.ToString());
            }
            var param = Parameter.CreateNew(value);
            return new Clause(param.Name, param);
        }

        public bool CanParse(Expression expression)
        {
            return expression is ConstantExpression;
        }
    }
}