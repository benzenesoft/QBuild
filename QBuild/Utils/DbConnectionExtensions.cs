using BenzeneSoft.QBuild.Clauses;
using System.Data;

namespace BenzeneSoft.QBuild.Utils
{
    public static class DbExtensions
    {
        public static IDbDataParameter CreateParameter(this IDbCommand command, Parameter parameter)
        {
            var dbParameter = command.CreateParameter();
            dbParameter.ParameterName = parameter.Name;
            dbParameter.Value = parameter.Value;
            return dbParameter;
        }

        public static IDbCommand CreateCommand(this IDbConnection connection, IClause clause)
        {
            var command = connection.CreateCommand();
            command.CommandText = clause.Text;
            foreach (var parameter in clause.Parameters)
            {
                var param = command.CreateParameter(parameter);
                command.Parameters.Add(param);
            }

            return command;
        }
    }
}
