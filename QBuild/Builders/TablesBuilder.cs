namespace BenzeneSoft.QBuild.Builders
{
    public class TablesBuilder : ITablesBuilder
    {
        private readonly INameResolver _nameResolver;
        private string _table;

        public TablesBuilder(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        public ISql Build()
        {
            return new Sql(_table);
        }

        public ITablesBuilder Table(string tableExpression)
        {
            _table = tableExpression;
            return this;
        }

        public ITablesBuilder Table<T>()
        {
            return Table(_nameResolver.Table(typeof(T)));
        }
    }
}