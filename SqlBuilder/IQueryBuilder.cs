namespace BenzeneSoft.SqlBuilder
{
    public interface IQueryBuilder : ISqlBuilder
    {
        IQueryBuilder Select(ISql select);
        IQueryBuilder From(ISql from);
        IQueryBuilder Where(ISql predicate);
    }
}