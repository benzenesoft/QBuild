using System.Data;
using System.Linq;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Utils
{
    public static class ClauseExtensions
    {
        public static bool IsEmpty(this IClause clause)
        {
            return string.IsNullOrEmpty(clause.Text) && !clause.Parameters.Any();
        }

        public static IDbCommand ToDbCommand(this IClause clause, IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = clause.Text;
            foreach (var parameter in clause.Parameters)
            {
                var param = parameter.ToDbParameter(command);
                command.Parameters.Add(param);
            }

            return command;
        }
    }
}
