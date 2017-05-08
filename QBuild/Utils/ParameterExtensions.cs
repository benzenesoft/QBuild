using System.Data;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Utils
{
    public static class ParameterExtensions
    {
        public static IDbDataParameter ToDbParameter(this Parameter parameter, IDbCommand command)
        {
            var dbParameter = command.CreateParameter();
            dbParameter.ParameterName = parameter.Name;
            dbParameter.Value = parameter.Value;
            return dbParameter;
        }
    }
}
