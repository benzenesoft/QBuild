using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface ILambdaResolver
    {
        IClause Parse(LambdaExpression expression, ClauseContext context);
    }
}