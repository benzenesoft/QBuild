using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.SqlBuilder.Builders
{
    public class SelectBuilder<T> : ISelectBuilder<T>
    {
        private readonly INameResolver _nameResolver;
        private readonly List<string> _columns;

        public SelectBuilder(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
            _columns = new List<string>();
        }

        public ISql Build()
        {
            var expression = string.Join("\n,", _columns);
            var sql = new Sql("SELECT ").Append(expression);
            return sql;
        }

        public ISelectBuilder<T> All()
        {
            return Columns("*");
        }

        public ISelectBuilder<T> Columns(params string[] expressions)
        {
            _columns.AddRange(expressions);
            return this;
        }

        public ISelectBuilder<T> Columns(params Expression<Func<T, object>>[] expressions)
        {
            var names = expressions.Select(_nameResolver.Column).ToArray();

            return Columns(names);
        }

        ISelectBuilder ISelectBuilder.All()
        {
            return All();
        }

        ISelectBuilder ISelectBuilder.Columns(params string[] expressions)
        {
            return Columns(expressions);
        }
    }
}