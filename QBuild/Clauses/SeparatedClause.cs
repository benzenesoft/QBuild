namespace BenzeneSoft.QBuild.Clauses
{
    public class SeparatedClause : MutableClause
    {
        private readonly IClause _separator;
        private bool _isEmpty;

        public SeparatedClause(IClause separator)
        {
            _separator = separator;
            _isEmpty = true;
        }
        
        public SeparatedClause AppendSeparated(IClause clause)
        {
            if (!_isEmpty)
                Append(_separator);

            _isEmpty = false;
            Append(clause);
            return this;
        }
    }
}