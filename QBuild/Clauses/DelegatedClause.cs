using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class DelegatedClause : IClause
    {
        protected readonly IClause Delegate;

        public DelegatedClause(IClause @delegate)
        {
            Delegate = @delegate;
        }

        public string Text => Delegate.Text;
        public IEnumerable<Parameter> Parameters => Delegate.Parameters;
        public bool IsEmpty => Delegate.IsEmpty;
    }
}