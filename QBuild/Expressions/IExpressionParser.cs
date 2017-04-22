
namespace BenzeneSoft.QBuild.Expressions
{
    public interface IExpressionParser<in TExpression>
    {
        ISql Parse(TExpression expression);
    }
}
