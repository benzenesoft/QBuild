namespace BenzeneSoft.QBuild.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        private ISql _select;
        private ISql _from;
        private ISql _where;
        private ISql _groupBy;
        private ISql _orderBy;

        public ISql Build()
        {
            var sql = new Sql()
                .Append(_select, appendLine:true)
                .Append(_from, appendLine: true)
                .Append(_where, appendLine: true)
                .Append(_groupBy, appendLine: true)
                .Append(_orderBy, appendLine: true);

            return sql;
        }

        public IQueryBuilder Select(ISql select)
        {
            _select = select;
            return this;
        }

        public IQueryBuilder From(ISql from)
        {
            _from = from;
            return this;
        }

        public IQueryBuilder Where(ISql where)
        {
            _where = where;
            return this;
        }

        public IQueryBuilder GroupBy(ISql groupBy)
        {
            _groupBy = groupBy;
            return this;
        }

        public IQueryBuilder OrderBy(ISql orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
    }
}