using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Builders
{
    public interface IQueryBuilder : IClauseBuilder
    {
        IQueryBuilder Select(IClause select);
        IQueryBuilder From(IClause from);
        IQueryBuilder Where(IClause where);
        IQueryBuilder GroupBy(IClause groupBy);
        IQueryBuilder Having(IClause having);
        IQueryBuilder OrderBy(IClause orderBy);
    }
}