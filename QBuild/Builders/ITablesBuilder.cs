namespace BenzeneSoft.QBuild.Builders
{
    //TODO feature: join tables
    public interface ITablesBuilder : ISqlBuilder
    {
        ITablesBuilder Table(string tableExpression);
    }

    public interface ITablesBuilder<T> : ITablesBuilder
    {
    }
}