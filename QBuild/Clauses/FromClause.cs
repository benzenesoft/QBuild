using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    //TODO feature: join tables
    public class FromClause : BaseClause
    {
        public override string Text => _delegate.Text;
        public override IEnumerable<Parameter> Parameters => _delegate.Parameters;

        private readonly MutableClause _delegate;

        public FromClause()
        {
            _delegate = new MutableClause();
        }

        public FromClause Table(IClause expression)
        {
            _delegate.Append(expression);
            return this;
        }

        public FromClause Table(string expression)
        {
            _delegate.AppendText(expression);
            return this;
        }
    }
}