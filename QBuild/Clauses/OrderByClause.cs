using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class OrderByClause : BaseClause
    {
        public override string Text => _delegate.Text;
        public override IEnumerable<Parameter> Parameters => _delegate.Parameters;
        private readonly SeparatedClause _delegate;

        public OrderByClause()
        {
            _delegate = new SeparatedClause(new MutableClause().Line().AppendText(","));
        }

        public OrderByClause Asc(string expression)
        {
            _delegate.AppendSeparated($"{expression} ASC");
            return this;
        }

        public OrderByClause Desc(string expression)
        {
            _delegate.AppendSeparated($"{expression} DESC");
            return this;
        }

        public OrderByClause Asc(IClause expression)
        {
            _delegate.AppendSeparated(expression).AppendText(" ASC");
            return this;
        }

        public OrderByClause Desc(IClause expression)
        {
            _delegate.AppendSeparated(expression).AppendText(" DESC");
            return this;
        }
    }
}