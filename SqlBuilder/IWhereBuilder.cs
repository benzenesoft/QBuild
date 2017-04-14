namespace BenzeneSoft.SqlBuilder
{
    public interface IWhereBuilder : ISqlBuilder
    {
        IWhereBuilder Or(params ISql[] predicates);
        IWhereBuilder And(params ISql[] predicates);
        IWhereBuilder Custom(ISql sql);
    }
}