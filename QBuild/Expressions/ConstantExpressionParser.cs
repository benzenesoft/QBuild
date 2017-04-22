using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ConstantExpressionParser : TypedExpressionParser<ConstantExpression>
    {
        protected override ISql ParseTyped(ConstantExpression expression)
        {
            var value = expression.Value;
            var param = Parameter.CreateNew(value);
            return new Sql(param.Name, param);
        }
    }
}