
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IExpressionParser
    {
        bool CanParse(Expression expression);
        IClause Parse(Expression expression);
    }
}
