using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Clauses
{
    public abstract class BaseClause : IClause
    {
        public virtual bool IsEmpty => string.IsNullOrEmpty(Text) && !Parameters.Any();
        public abstract IEnumerable<Parameter> Parameters { get; }
        public abstract string Text { get; }

        public override string ToString()
        {
            return Text;
        }
    }
}