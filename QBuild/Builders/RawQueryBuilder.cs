using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Builders
{
    public class RawQueryBuilder : IClauseBuilder
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

        public RawQueryBuilder Select(IClause select)
        {
            _select = select;
            return this;
        }

        public RawQueryBuilder From(IClause from)
        {
            _from = from;
            return this;
        }

        public RawQueryBuilder Where(IClause where)
        {
            _where = where;
            return this;
        }

        public RawQueryBuilder GroupBy(IClause groupBy)
        {
            _groupBy = groupBy;
            return this;
        }

        public RawQueryBuilder Having(IClause having)
        {
            _having = having;
            return this;
        }

        public RawQueryBuilder OrderBy(IClause orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
    }
}