using System.Linq.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ConstantExpressionParser : IExpressionParser
    {
        public ISql Parse(Expression expression)
        {
            var value = (expression as ConstantExpression).Value;
            var param = Parameter.CreateNew(value);
            return new Sql(param.Name, param);
        }

        public bool CanParse(Expression expression)
        {
            return expression is ConstantExpression;
        }
    }
}