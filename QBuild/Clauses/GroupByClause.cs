using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class GroupByClause : BaseClause
    {
        public override string Text => _delegate.Text;
        public override IEnumerable<Parameter> Parameters => _delegate.Parameters;

        private readonly SeparatedClause _delegate;

        public GroupByClause()
        {
            _delegate = new SeparatedClause(new MutableClause().Line().AppendText(","));
        }

        public GroupByClause Column(string expression)
        {
            _delegate.AppendSeparated(expression);
            return this;
        }

        public GroupByClause Column(IClause expression)
        {
            _delegate.AppendSeparated(expression);
            return this;
        }
    }
}