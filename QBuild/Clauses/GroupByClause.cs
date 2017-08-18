using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class GroupByClause : IClause
    {
        public string Text => _delegate.Text;
        public IEnumerable<Parameter> Parameters => _delegate.Parameters;
        public bool IsEmpty => _delegate.IsEmpty;

        private readonly SeparatedClause _delegate;

        public GroupByClause()
        {
            _delegate = new SeparatedClause(new MutableClause().Line().Append(","));
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