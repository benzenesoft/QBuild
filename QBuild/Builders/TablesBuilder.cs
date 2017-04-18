namespace BenzeneSoft.QBuild.Builders
{
    public class TablesBuilder<T> : ITablesBuilder<T>
    {
        private readonly INameResolver _nameResolver;
        private string _table;

        public TablesBuilder(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        public ISql Build()
        {
            var table = _table ?? _nameResolver.Table(typeof(T));
            return new Sql(table);
        }

        ITablesBuilder ITablesBuilder.Table(string tableExpression)
        {
            return Table(tableExpression);
        }

        public ITablesBuilder<T> Table(string tableExpression)
        {
            _table = tableExpression;
            return this;
        }
    }
}