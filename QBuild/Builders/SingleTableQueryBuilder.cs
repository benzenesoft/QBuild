using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public class SingleTableQueryBuilder<T> : ISqlBuilder
    {
        private IColumnsBuilder _selectBuilder;
        private readonly ITablesBuilder _tablesBuilder;
        private Expression<Func<T, bool>> _wherePredicate;
        private IColumnsBuilder _groupByBuilder;
        private Expression<Func<T, bool>> _havingPredicate;
        private IOrderByBuilder _orderByBuilder;
        private readonly IPredicateParser _predicateParser;

        public SingleTableQueryBuilder(INameResolver nameResolver)
            : this(nameResolver, new ParserLookup(nameResolver))
        {
        }

        public SingleTableQueryBuilder(INameResolver nameResolver, IParserLookup lookup)
            : this(new ColumnsBuilder(nameResolver)
                  , new TablesBuilder(nameResolver)
                  , new ColumnsBuilder(nameResolver)
                  , new OrderByBuilder(nameResolver)
                  , new PredicateParser(lookup))
        {
        }

        public SingleTableQueryBuilder(IColumnsBuilder selectBuilder
            , ITablesBuilder tablesBuilder
            , IColumnsBuilder groupByBuilder
            , IOrderByBuilder orderByBuilder
            , IPredicateParser predicateParser)
        {
            _selectBuilder = selectBuilder;
            _tablesBuilder = tablesBuilder;
            _groupByBuilder = groupByBuilder;
            _orderByBuilder = orderByBuilder;
            _predicateParser = predicateParser;
        }

        public SingleTableQueryBuilder<T> SelectAll()
        {
            _selectBuilder.All();
            return this;
        }

        public SingleTableQueryBuilder<T> Select(params Expression<Func<T, object>>[] expressions)
        {
            _selectBuilder.Columns(expressions);
            return this;
        }

        public SingleTableQueryBuilder<T> Where(Expression<Func<T, bool>> predicate)
        {
            _wherePredicate = predicate;
            return this;
        }

        public SingleTableQueryBuilder<T> GroupBy(params Expression<Func<T, object>>[] expressions)
        {
            _groupByBuilder.Columns(expressions);
            return this;
        }

        public SingleTableQueryBuilder<T> Having(Expression<Func<T, bool>> predicate)
        {
            _havingPredicate = predicate;
            return this;
        }

        public SingleTableQueryBuilder<T> OrderByAsc(params Expression<Func<T, object>>[] orderProperty)
        {
            _orderByBuilder.Asc(orderProperty);
            return this;
        }

        public SingleTableQueryBuilder<T> OrderByDesc(params Expression<Func<T, object>>[] orderProperty)
        {
            _orderByBuilder.Desc(orderProperty);
            return this;
        }

        public ISql Build()
        {
            var queryBuilder = new QueryBuilder();
            var sql = queryBuilder
                .Select(_selectBuilder.Build())
                .From(_tablesBuilder.Table<T>().Build())
                .Where(_predicateParser.Parse(_wherePredicate))
                .GroupBy(_groupByBuilder.Build())
                .Having(_predicateParser.Parse(_havingPredicate))
                .OrderBy(_orderByBuilder.Build())
                .Build();

            return sql;
        }
    }
}
