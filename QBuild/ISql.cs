using System.Collections.Generic;
using System.Data;

namespace BenzeneSoft.QBuild
{
    public interface ISql
    {
        string SqlText { get; }
        IEnumerable<Parameter> Parameters { get; }
        IDbCommand CreateDbCommand(IDbConnection connection);
    }
}