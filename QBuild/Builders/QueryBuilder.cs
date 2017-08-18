using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Builders
{
    public class QueryBuilder : IClauseBuilder
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

            AppendIfNotEmpty("SELECT ", _select, clause);
            AppendIfNotEmpty("FROM ", _from, clause);
            AppendIfNotEmpty("WHERE ", _where, clause);
            AppendIfNotEmpty("GROUP BY ", _groupBy, clause);
            AppendIfNotEmpty("HAVING ", _having, clause);
            AppendIfNotEmpty("ORDER BY ", _orderBy, clause);

            return clause;
        }

        private void AppendIfNotEmpty(string prefix, IClause append, MutableClause appendTo)
        {
            if (append != null && !append.IsEmpty)
                appendTo.AppendText(prefix).Append(append).Line();
        }

        public QueryBuilder Select(IClause select)
        {
            _select = select;
            return this;
        }

        public QueryBuilder From(IClause from)
        {
            _from = from;
            return this;
        }

        public QueryBuilder Where(IClause where)
        {
            _where = where;
            return this;
        }

        public QueryBuilder GroupBy(IClause groupBy)
        {
            _groupBy = groupBy;
            return this;
        }

        public QueryBuilder Having(IClause having)
        {
            _having = having;
            return this;
        }

        public QueryBuilder OrderBy(IClause orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
    }
}