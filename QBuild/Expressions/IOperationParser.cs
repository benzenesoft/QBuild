using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IOperationParser
    {
        bool CanParse(ExpressionType operation);
        IClause Parse(ExpressionType operation, params IClause[] operands);
    }
}
