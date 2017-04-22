using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ConstantExpressionParser : ExpressionParser<ConstantExpression>
    {
        protected override ISql ParseImpl(ConstantExpression expression)
        {
            var value = expression.Value;
            var param = Parameter.CreateNew(value);
            return new Sql(param.Name, param);
        }
    }
}