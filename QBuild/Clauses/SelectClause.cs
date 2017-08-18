using System.Collections.Generic;

namespace BenzeneSoft.QBuild.Clauses
{
    public class SelectClause : BaseClause
    {
        public override string Text => _delegate.Text;
        public override IEnumerable<Parameter> Parameters => _delegate.Parameters;

        private readonly SeparatedClause _delegate;

        public SelectClause()
        {
            _delegate = new SeparatedClause(new MutableClause().Line().Append(","));
        }

        public SelectClause Distinct()
        {
            _delegate.Prepend("DISTINCT ");
            return this;
        }

        public SelectClause All()
        {
            _delegate.AppendSeparated(new Clause("*"));
            return this;
        }

        public SelectClause Column(string expression)
        {
            _delegate.AppendSeparated(expression);
            return this;
        }

        public SelectClause Column(IClause expression)
        {
            _delegate.AppendSeparated(expression);
            return this;
        }

        public SelectClause ColumnAs(IClause expression, IClause alias)
        {
            _delegate.AppendSeparated(expression)
                .Append(" as ")
                .Append(alias);
            return this;
        }
    }
}
