
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IExpressionParser
    {
        bool CanParse(Expression expression);
        ISql Parse(Expression expression);
    }
}
