using BenzeneSoft.QBuild.Utils;
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
            _delegate = new SeparatedClause(new MutableClause().Line().AppendText(","));
        }

        public SelectClause Distinct()
        {
            _delegate.PrependText("DISTINCT ");
            return this;
        }

        public SelectClause All()
        {
            _delegate.AppendSeparated("*".ToClause());
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
                .AppendText(" as ")
                .Append(alias);
            return this;
        }
    }
}
