using System.Linq.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface ILambdaParser
    {
        ISql Parse(LambdaExpression predicate);
    }
}