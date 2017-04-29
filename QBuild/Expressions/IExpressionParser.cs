
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IExpressionParser
    {
        bool CanParse(Expression expression);
        ISql Parse(Expression expression);
    }
}
