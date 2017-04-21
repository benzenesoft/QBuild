using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Predicates
{
    public interface IOperatorResolver
    {
        string this[ExpressionType expressionType] { get; }
    }
}
