using BenzeneSoft.QBuild.Clauses;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IParserLookup
    {
        IExpressionParser Lookup(Expression expression);
        IOperationParser Lookup(ExpressionType operation);

        IClause Parse(Expression expression, ClauseContext context);
        IClause Parse(ExpressionType operation, params IClause[] operands);
    }
}
