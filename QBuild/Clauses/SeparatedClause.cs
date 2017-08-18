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
            AppendSeparator();
            Append(clause);
            return this;
        }

        public SeparatedClause AppendSeparated(string expression)
        {
            AppendSeparator();
            AppendText(expression);
            return this;
        }

        private void AppendSeparator()
        {
            if (!_isEmpty)
                Append(_separator);

            _isEmpty = false;
        }
    }
}