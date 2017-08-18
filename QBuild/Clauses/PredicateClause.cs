namespace BenzeneSoft.QBuild.Clauses
{
    public class PredicateClause : DelegatedClause
    {
        private readonly MutableClause _delegate;
        public PredicateClause() : base(new MutableClause())
        {
            _delegate = (MutableClause) Delegate;
        }

        public PredicateClause And(IClause predicate)
        {
            return AppendPredicate("AND", predicate);
        }

        public PredicateClause Or(IClause predicate)
        {
            return AppendPredicate("OR", predicate);
        }

        private PredicateClause AppendPredicate(string condition, IClause predicate)
        {
            if (!IsEmpty)
                _delegate.AppendText($" {condition} ");
            _delegate
                .AppendText("(")
                .Append(predicate)
                .AppendText(")");
            return this;
        }
    }
}