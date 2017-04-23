namespace BenzeneSoft.QBuild.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        private ISql _select;
        private ISql _from;
        private ISql _where;
        private ISql _groupBy;
        private ISql _having;
        private ISql _orderBy;

        public ISql Build()
        {
            var sql = new Sql();

            AppendIfNotNull("SELECT ", _select, sql);
            AppendIfNotNull("FROM ", _from, sql);
            AppendIfNotNull("WHERE ", _where, sql);
            AppendIfNotNull("GROUP BY ", _groupBy, sql);
            AppendIfNotNull("HAVING ", _having, sql);
            AppendIfNotNull("ORDER BY ", _orderBy, sql);

            return sql;
        }

        private void AppendIfNotNull(string prefix, ISql append, Sql appendTo)
        {
            if (append != null)
                appendTo.Append(prefix).Append(append).Line();
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

        public IQueryBuilder Having(ISql having)
        {
            _having = having;
            return this;
        }

        public IQueryBuilder OrderBy(ISql orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
    }
}