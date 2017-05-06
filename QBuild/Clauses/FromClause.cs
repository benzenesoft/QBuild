using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    //TODO feature: join tables
    public class FromClause : IClause
    {
        public string Text => _delegate.Text;
        public IEnumerable<Parameter> Parameters => _delegate.Parameters;

        private readonly MutableClause _delegate;

        public FromClause()
        {
            _delegate = new MutableClause();
        }

        public FromClause From(IClause expression)
        {
            _delegate.Append(expression);
            return this;
        }
    }
}