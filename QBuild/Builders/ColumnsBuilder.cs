using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public class ColumnsBuilder<T> : IColumnsBuilder<T>
    {
        private readonly INameResolver _nameResolver;
        private readonly bool _appendLine;
        private readonly List<string> _columns;

        public ColumnsBuilder(INameResolver nameResolver, bool appendLine = false)
        {
            _nameResolver = nameResolver;
            _appendLine = appendLine;
            _columns = new List<string>();
        }

        public ISql Build()
        {
            if (_columns.Count <= 0) return new Sql();

            var sql = new Sql();
            sql.Append(new Sql(_columns[0]), _appendLine);
            for (var i = 1; i < _columns.Count; i++)
            {
                sql.Append(",").Append(new Sql(_columns[i]), _appendLine);
            }

            return sql;
        }

        public IColumnsBuilder<T> All()
        {
            return Columns("*");
        }

        public IColumnsBuilder<T> Columns(params string[] expressions)
        {
            _columns.AddRange(expressions);
            return this;
        }

        public IColumnsBuilder<T> Columns(params Expression<Func<T, object>>[] expressions)
        {
            var names = expressions.Select(_nameResolver.Column).ToArray();

            return Columns(names);
        }

        IColumnsBuilder IColumnsBuilder.All()
        {
            return All();
        }

        IColumnsBuilder IColumnsBuilder.Columns(params string[] expressions)
        {
            return Columns(expressions);
        }
    }
}