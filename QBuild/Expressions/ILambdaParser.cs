using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface ILambdaParser
    {
        IClause Parse(LambdaExpression expression, ClauseContext context);
    }
}