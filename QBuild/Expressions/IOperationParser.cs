using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IOperationParser
    {
        bool CanParse(ExpressionType operation);
        ISql Parse(ExpressionType operation, params ISql[] operands);
    }
}
