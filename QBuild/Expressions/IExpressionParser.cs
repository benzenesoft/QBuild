using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IExpressionParser
    {
        ISql Parse(Expression expression);
    }
}
