using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Clauses
{
    public class Clause : IClause
    {
        public Clause(string text, IEnumerable<Parameter> parameters)
        {
            Text = text;
            Parameters = parameters;
        }

        public Clause(string text)
            : this(text, Enumerable.Empty<Parameter>())
        {
        }

        public string Text { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}