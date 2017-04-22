using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IParserLookup
    {
        IExpressionParser FindParser(Expression expression);
    }
}
