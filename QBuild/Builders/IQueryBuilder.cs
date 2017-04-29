namespace BenzeneSoft.QBuild.Builders
{
    public interface IQueryBuilder : ISqlBuilder
    {
        IQueryBuilder Select(ISql select);
        IQueryBuilder From(ISql from);
        IQueryBuilder Where(ISql where);
        IQueryBuilder GroupBy(ISql groupBy);
        IQueryBuilder Having(ISql having);
        IQueryBuilder OrderBy(ISql orderBy);
    }
}