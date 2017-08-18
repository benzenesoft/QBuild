using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public sealed class Clause : BaseClause
    {
        public Clause(string text, params Parameter[] parameters)
        {
            Text = text;
            Parameters = parameters;
        }

        public override string Text { get; }
        public override IEnumerable<Parameter> Parameters { get; }
    }
}