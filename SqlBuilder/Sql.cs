using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BenzeneSoft.SqlBuilder
{
    public class Sql : ISql
    {
        private readonly StringBuilder _sqlTextBuilder;
        private readonly List<Parameter> _parameters;

        public Sql() : this(string.Empty)
        {
        }

        public Sql(string sqlText, params Parameter[] parameters)
        {
            _sqlTextBuilder = new StringBuilder(sqlText);
            _parameters = new List<Parameter>(parameters);
        }

        public string SqlText => _sqlTextBuilder.ToString();

        public IEnumerable<Parameter> Parameters => _parameters;

        public Sql Append(ISql sql, bool wrapParanthesis = false)
        {
            var text = sql.SqlText;
            if (wrapParanthesis)
            {
                text = $"({text})";
            }

            _sqlTextBuilder.Append(text);
            _parameters.AddRange(sql.Parameters);
            return this;
        }

        public Sql Text(string sqlText)
        {
            _sqlTextBuilder.Append(sqlText);
            return this;
        }

        public Sql Line()
        {
            _sqlTextBuilder.AppendLine();
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