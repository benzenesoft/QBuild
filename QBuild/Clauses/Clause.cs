using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Clauses
{
    public class Clause : IClause
    {
        public Clause(string sqlText, IEnumerable<Parameter> parameters)
        {
            SqlText = sqlText;
            Parameters = parameters;
        }

        public Clause(string sqlText)
            : this(sqlText, Enumerable.Empty<Parameter>())
        {
        }

        public string SqlText { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}