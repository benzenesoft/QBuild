using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class Clause : IClause
    {
        private readonly Parameter[] _parameters;

        public Clause(string text, params Parameter[] parameters)
        {
            Text = text;
            _parameters = parameters;
        }

        public virtual string Text { get; }
        public virtual IEnumerable<Parameter> Parameters => _parameters;
        public virtual bool IsEmpty => string.IsNullOrEmpty(Text) && _parameters.Length == 0;

        public override string ToString()
        {
            return Text;
        }
    }
}