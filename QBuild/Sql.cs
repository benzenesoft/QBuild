using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BenzeneSoft.QBuild.Builders;

namespace BenzeneSoft.QBuild
{
    public class Sql : ISql
    {
        private readonly StringBuilder _sqlTextBuilder;
        private readonly List<Parameter> _parameters;

        public Sql(string sqlText, params Parameter[] parameters)
        {
            _sqlTextBuilder = new StringBuilder(sqlText);
            _parameters = new List<Parameter>(parameters);
        }

        public Sql() : this(string.Empty) { }
        public Sql(ISql sql) : this(sql.SqlText, sql.Parameters.ToArray()) { }
        public Sql(ISqlBuilder builder) : this(builder.Build()) { }

        public string SqlText => _sqlTextBuilder.ToString();

        public IEnumerable<Parameter> Parameters => _parameters;

        public Sql Append(ISql sql, bool wrapParanthesis = false)
        {
            if (sql == null) return this;

            var text = sql.SqlText;
            if (wrapParanthesis)
            {
                text = $"({text})";
            }

            _sqlTextBuilder.Append(text);
            _parameters.AddRange(sql.Parameters);
            return this;
        }

        public Sql Append(string sqlText)
        {
            _sqlTextBuilder.Append(sqlText);
            return this;
        }

        public Sql Line()
        {
            _sqlTextBuilder.Append(Environment.NewLine);
            return this;
        }

        public IDbCommand CreateDbCommand(IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = SqlText;
            foreach (var param in Parameters)
            {
                command.Parameters.Add(param.CreateDbParameter(command));
            }
            return command;
        }
    }
}