using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Builders
{
    public interface ISqlBuilder
    {
        IClause Build();
    }
}