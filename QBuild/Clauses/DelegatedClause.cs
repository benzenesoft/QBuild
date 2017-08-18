using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class DelegatedClause : BaseClause
    {
        protected readonly IClause Delegate;

        public DelegatedClause(IClause @delegate)
        {
            Delegate = @delegate;
        }

        public override string Text => Delegate.Text;
        public override IEnumerable<Parameter> Parameters => Delegate.Parameters;
    }
}