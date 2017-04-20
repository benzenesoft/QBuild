namespace BenzeneSoft.QBuild.Builders
{
    public interface IPredicateBuilder : ISqlBuilder
    {
        IPredicateBuilder Or(ISql predicate);
        IPredicateBuilder And(ISql predicate);
        IPredicateBuilder Begin(ISql predicate);
    }
}