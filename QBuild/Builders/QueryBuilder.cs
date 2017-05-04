using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        private IClause _select;
        private IClause _from;
        private IClause _where;
        private IClause _groupBy;
        private IClause _having;
        private IClause _orderBy;

        public IClause Build()
        {
            var sql = new Clause();

            AppendIfNotNull("SELECT ", _select, sql);
            AppendIfNotNull("FROM ", _from, sql);
            AppendIfNotNull("WHERE ", _where, sql);
            AppendIfNotNull("GROUP BY ", _groupBy, sql);
            AppendIfNotNull("HAVING ", _having, sql);
            AppendIfNotNull("ORDER BY ", _orderBy, sql);

            return sql;
        }

        private void AppendIfNotNull(string prefix, IClause append, Clause appendTo)
        {
            if (append != null)
                appendTo.Append(prefix).Append(append).Line();
        }

        public IQueryBuilder Select(IClause select)
        {
            _select = select;
            return this;
        }

        public IQueryBuilder From(IClause from)
        {
            _from = from;
            return this;
        }

        public IQueryBuilder Where(IClause where)
        {
            _where = where;
            return this;
        }

        public IQueryBuilder GroupBy(IClause groupBy)
        {
            _groupBy = groupBy;
            return this;
        }

        public IQueryBuilder Having(IClause having)
        {
            _having = having;
            return this;
        }

        public IQueryBuilder OrderBy(IClause orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
    }
}