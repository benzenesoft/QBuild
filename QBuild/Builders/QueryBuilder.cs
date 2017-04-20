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
                .Append("SELECT ").Append(_select, true)
                .Append("FROM ").Append(_from, true);

            if (_where != null)
                sql.Append("WHERE ").Append(_where, true);

            if (_groupBy != null)
                sql.Append("GROUP BY ").Append(_groupBy, true);

            if (_orderBy != null)
                sql.Append("ORDER BY ").Append(_orderBy, true);

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