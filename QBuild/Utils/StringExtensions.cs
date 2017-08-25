using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Utils
{
    public static class StringExtensions
    {
        public static Clause ToClause(this string text)
        {
            return new Clause(text);
        }
    }
}
