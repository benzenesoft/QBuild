using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IParserLookup
    {
        IExpressionParser this[Expression expression] { get; }
        IOperationParser this[ExpressionType operation] { get; }
    }
}
