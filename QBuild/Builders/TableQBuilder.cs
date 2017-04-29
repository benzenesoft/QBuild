using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Builders
{
    public class TableQBuilder<T> : ISqlBuilder
    {
        private IColumnsBuilder _selectBuilder;
        private readonly ITablesBuilder _tablesBuilder;
        private Expression<Func<T, bool>> _wherePredicate;
        private IColumnsBuilder _groupByBuilder;
        private Expression<Func<T, bool>> _havingPredicate;
        private IOrderByBuilder _orderByBuilder;
        private readonly ILambdaParser _lambdaParser;

        public TableQBuilder(INameResolver nameResolver)
            : this(new LambdaParser(new ParserLookup(nameResolver)), nameResolver)
        {
        }

        public TableQBuilder(ILambdaParser lambdaParser, INameResolver nameResolver)
            : this(new ColumnsBuilder(lambdaParser)
                  , new TablesBuilder(nameResolver)
                  , new ColumnsBuilder(lambdaParser)
                  , new OrderByBuilder(lambdaParser)
                  , lambdaParser)
        {
        }

        public TableQBuilder(IColumnsBuilder selectBuilder
            , ITablesBuilder tablesBuilder
            , IColumnsBuilder groupByBuilder
            , IOrderByBuilder orderByBuilder
            , ILambdaParser lambdaParser)
        {
            _selectBuilder = selectBuilder;
            _tablesBuilder = tablesBuilder;
            _groupByBuilder = groupByBuilder;
            _orderByBuilder = orderByBuilder;
            _lambdaParser = lambdaParser;
        }

        public TableQBuilder<T> SelectAll()
        {
            _selectBuilder.All();
            return this;
        }

        public TableQBuilder<T> Select(params Expression<Func<T, object>>[] expressions)
        {
            _selectBuilder.Columns(expressions);
            return this;
        }

        public TableQBuilder<T> Where(Expression<Func<T, bool>> predicate)
        {
            _wherePredicate = predicate;
            return this;
        }

        public TableQBuilder<T> GroupBy(params Expression<Func<T, object>>[] expressions)
        {
            _groupByBuilder.Columns(expressions);
            return this;
        }

        public TableQBuilder<T> Having(Expression<Func<T, bool>> predicate)
        {
            _havingPredicate = predicate;
            return this;
        }

        public TableQBuilder<T> OrderByAsc(params Expression<Func<T, object>>[] orderProperty)
        {
            _orderByBuilder.Asc(orderProperty);
            return this;
        }

        public TableQBuilder<T> OrderByDesc(params Expression<Func<T, object>>[] orderProperty)
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
                .Where(_lambdaParser.Parse(_wherePredicate))
                .GroupBy(_groupByBuilder.Build())
                .Having(_lambdaParser.Parse(_havingPredicate))
                .OrderBy(_orderByBuilder.Build())
                .Build();

            return sql;
        }
    }
}
