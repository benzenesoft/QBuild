using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ConstantExpressionParser : IExpressionParser
    {
        public IClause Parse(Expression expression)
        {
            var value = (expression as ConstantExpression).Value;
            var param = Parameter.CreateNew(value);
            return new Clause(param.Name, param);
        }

        public bool CanParse(Expression expression)
        {
            return expression is ConstantExpression;
        }
    }
}