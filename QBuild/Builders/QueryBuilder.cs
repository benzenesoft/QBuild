namespace BenzeneSoft.QBuild.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        private ISql _select;
        private ISql _from;
        private ISql _predicate;
        private ISql _orderBy;

        public ISql Build()
        {
            var sql = new Sql()
                .Append(_select).Line()
                .Append(_from).Line()
                .Append(_predicate).Line()
                .Append(_orderBy).Line();

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

        public IQueryBuilder Where(ISql predicate)
        {
            _predicate = predicate;
            return this;
        }

        public IQueryBuilder OrderBy(ISql orderBy)
        {
            _orderBy = orderBy;
            return this;
        }
    }
}