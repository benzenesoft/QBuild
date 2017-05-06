using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public interface IClause
    {
        string Text { get; }
        IEnumerable<Parameter> Parameters { get; }
    }
}