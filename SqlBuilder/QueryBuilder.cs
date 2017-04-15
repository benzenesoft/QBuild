using System;

namespace BenzeneSoft.SqlBuilder
{
    public class QueryBuilder<T> : IQueryBuilder<T>
    {
        private readonly ISelectBuilder<T> _selectBuilder;
        private readonly IFromBuilder<T> _fromBuilder;

        public QueryBuilder(ISelectBuilder<T> selectBuilder, IFromBuilder<T> fromBuilder)
        {
            _selectBuilder = selectBuilder;
            _fromBuilder = fromBuilder;
        }

        public ISql Build()
        {
            var sql = new Sql();
            sql.Append(_selectBuilder.Build()).Line();

            return sql;
        }

        public IQueryBuilder Select(Action<ISelectBuilder> build)
        {
            build.Invoke(_selectBuilder);
            return this;
        }

        public IQueryBuilder<T> Select(Action<ISelectBuilder<T>> build)
        {
            build.Invoke(_selectBuilder);
            return this;
        }

        public IQueryBuilder From(Action<IFromBuilder> build)
        {
            build.Invoke(_fromBuilder);
            return this;
        }

        public IQueryBuilder From(Action<IFromBuilder<T>> build)
        {
            build.Invoke(_fromBuilder);
            return this;
        }

        public IQueryBuilder Where(Action<IWhereBuilder, IPredicateFactory> builder)
        {
            throw new NotImplementedException();
        }

        public IQueryBuilder<T> Where(Action<IWhereBuilder, IPredicateFactory<T>> builder)
        {
            throw new NotImplementedException();
        }
    }
}