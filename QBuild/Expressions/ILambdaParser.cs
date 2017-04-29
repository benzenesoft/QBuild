using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface ILambdaParser
    {
        ISql Parse(LambdaExpression predicate);
    }
}