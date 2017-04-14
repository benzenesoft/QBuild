using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BenzeneSoft.SqlBuilder
{
    public class Sql : ISql
    {
        private readonly StringBuilder _sqlTextBuilder;
        private readonly List<Parameter> _parameters;

        public Sql()
        {
            _sqlTextBuilder = new StringBuilder();
            _parameters = new List<Parameter>();
        }

        public string SqlText => _sqlTextBuilder.ToString();

        public IEnumerable<Parameter> Parameters => _parameters;

        public Sql Append(ISql sql)
        {
            _sqlTextBuilder.Append(sql.SqlText);
            _parameters.AddRange(sql.Parameters);
            return this;
        }

        public Sql Prepend(ISql sql)
        {
            _sqlTextBuilder.Insert(0, sql.SqlText);
            _parameters.InsertRange(0, sql.Parameters);
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