using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Sqls
{
    public interface ISql
    {
        string SqlText { get; }
        IEnumerable<Parameter> Parameters { get; }
    }
}