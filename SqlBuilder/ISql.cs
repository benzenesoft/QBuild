using System.Collections.Generic;
using System.Data;

namespace BenzeneSoft.SqlBuilder
{
    public interface ISql
    {
        string SqlText { get; }
        IEnumerable<Parameter> Parameters { get; }
        IDbCommand CreateCommand(IDbConnection connection);
    }
}