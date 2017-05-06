using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class Clause : IClause
    {
        public Clause(string text, params Parameter[] parameters)
        {
            Text = text;
            Parameters = parameters;
        }

        public string Text { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}