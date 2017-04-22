using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ConstantExpressionParser : IExpressionParser<ConstantExpression>
    {
        public ISql Parse(ConstantExpression expression)
        {
            var value = expression.Value;
            var param = Parameter.CreateNew(value);
            return new Sql(param.Name, param);
        }
    }
}