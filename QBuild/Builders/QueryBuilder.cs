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
            var clause = new MutableClause();

            AppendIfNotNull("SELECT ", _select, clause);
            AppendIfNotNull("FROM ", _from, clause);
            AppendIfNotNull("WHERE ", _where, clause);
            AppendIfNotNull("GROUP BY ", _groupBy, clause);
            AppendIfNotNull("HAVING ", _having, clause);
            AppendIfNotNull("ORDER BY ", _orderBy, clause);

            return clause;
        }

        private void AppendIfNotNull(string prefix, IClause append, MutableClause appendTo)
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