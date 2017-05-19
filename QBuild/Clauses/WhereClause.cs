namespace BenzeneSoft.QBuild.Clauses
{
    public class WhereClause : DelegatedClause
    {
        private readonly MutableClause _delegate;
        public WhereClause() : base(new MutableClause())
        {
            _delegate = (MutableClause) Delegate;
        }

        public WhereClause And(IClause predicate)
        {
            return AppendPredicate(" AND ", predicate);
        }

        public WhereClause Or(IClause predicate)
        {
            return AppendPredicate(" OR ", predicate);
        }

        private WhereClause AppendPredicate(string condition, IClause predicate)
        {
            if (!IsEmpty)
                _delegate.Append($" {condition} ");
            _delegate.Append(new MutableClause(predicate).WrapParentheses());
            return this;
        }
    }
}