using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IOperatorResolver
    {
        string this[ExpressionType expressionType] { get; }
    }
}
