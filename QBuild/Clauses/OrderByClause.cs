using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class OrderByClause : IClause
    {
        public string Text => _delegate.Text;
        public IEnumerable<Parameter> Parameters => _delegate.Parameters;

        private readonly SeparatedClause _delegate;

        public OrderByClause()
        {
            _delegate = new SeparatedClause(new MutableClause().Line().Append(","));
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
            _delegate.AppendSeparated(expression).Append(" ASC");
            return this;
        }

        public OrderByClause Desc(IClause expression)
        {
            _delegate.AppendSeparated(expression).Append(" DESC");
            return this;
        }
    }
}