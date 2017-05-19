using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class Clause : IClause
    {
        private Parameter[] _parameters;

        public Clause(string text, params Parameter[] parameters)
        {
            Text = text;
            _parameters = parameters;
        }

        public string Text { get; }
        public IEnumerable<Parameter> Parameters => _parameters;
        public bool IsEmpty => string.IsNullOrEmpty(Text) && _parameters.Length ==0;
    }
}