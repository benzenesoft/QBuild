using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public interface IClause
    {
        string SqlText { get; }
        IEnumerable<Parameter> Parameters { get; }
    }
}