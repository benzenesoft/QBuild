using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.NameResolvers;

namespace BenzeneSoft.QBuild.Builders
{
    public class LambdaQueryBuilder : IClauseBuilder
    {
        private readonly ILambdaParser _parser;
        private readonly INameResolver _nameResolver;
        private SelectClause _select;
        private FromClause _from;
        private PredicateClause _where;
        private GroupByClause _groupBy;
        private PredicateClause _having;
        private OrderByClause _orderBy;

        public LambdaQueryBuilder()
            : this(new AsIsNameResolver())
        {
        }
        public LambdaQueryBuilder(INameResolver nameResolver)
            : this(new ParserLookup(nameResolver), nameResolver)
        {
        }

        public LambdaQueryBuilder(IParserLookup lookup, INameResolver nameResolver)
            : this(new LambdaParser(lookup), nameResolver)
        {
        }

        public LambdaQueryBuilder(ILambdaParser parser, INameResolver nameResolver)
        {
            _parser = parser;
            _nameResolver = nameResolver;
            _select = new SelectClause();
            _from = new FromClause();
            _where = new PredicateClause();
            _groupBy = new GroupByClause();
            _having = new PredicateClause();
            _orderBy = new OrderByClause();
        }

        public IClause Build()
        {
            var queryBuilder = new QueryBuilder()
                .Select(_select)
                .From(_from)
                .Where(_where)
                .GroupBy(_groupBy)
                .Having(_having)
                .OrderBy(_orderBy);

            return queryBuilder.Build();
        }

        public LambdaQueryBuilder SelectAll()
        {
            _select.All();
            return this;
        }

        public LambdaQueryBuilder SelectAs<T>(Expression<Func<T, object>> expression, string alias)
        {
            _select.ColumnAs(_parser.Parse(expression, ClauseContext.Select), new Clause(alias));
            return this;
        }

        public LambdaQueryBuilder Select<T>(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                _select.Column(_parser.Parse(expression, ClauseContext.Select));
            }
            return this;
        }

        public LambdaQueryBuilder From<T>()
        {
            _from.Table(_nameResolver.Resolve(typeof(T)));
            return this;
        }

        public LambdaQueryBuilder Where<T>(Expression<Func<T, bool>> predicate)
        {
            _where.And(_parser.Parse(predicate, ClauseContext.Where));
            return this;
        }

        public LambdaQueryBuilder AndWhere<T>(Expression<Func<T, bool>> predicate)
        {
            _where.And(_parser.Parse(predicate, ClauseContext.Where));
            return this;
        }

        public LambdaQueryBuilder OrWhere<T>(Expression<Func<T, bool>> predicate)
        {
            _where.Or(_parser.Parse(predicate, ClauseContext.Where));
            return this;
        }

        public LambdaQueryBuilder GroupBy<T>(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                _groupBy.Column(_parser.Parse(expression, ClauseContext.GroupBy));
            }
            return this;
        }

        public LambdaQueryBuilder Having<T>(Expression<Func<T, bool>> predicate)
        {
            _having.And(_parser.Parse(predicate, ClauseContext.Having));
            return this;
        }

        public LambdaQueryBuilder AndHaving<T>(Expression<Func<T, bool>> predicate)
        {
            _having.And(_parser.Parse(predicate, ClauseContext.Having));
            return this;
        }

        public LambdaQueryBuilder OrHaving<T>(Expression<Func<T, bool>> predicate)
        {
            _having.Or(_parser.Parse(predicate, ClauseContext.Having));
            return this;
        }

        public LambdaQueryBuilder OrderByAsc<T>(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                _orderBy.Asc(_parser.Parse(expression, ClauseContext.OrderBy));
            }
            return this;
        }

        public LambdaQueryBuilder OrderByDesc<T>(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                _orderBy.Desc(_parser.Parse(expression, ClauseContext.OrderBy));
            }
            return this;
        }
    }
}
