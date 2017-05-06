using System.Linq;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Utils
{
    public static class ClauseExtensions
    {
        public static bool IsEmpty(this IClause clause)
        {
            return string.IsNullOrEmpty(clause.Text) && !clause.Parameters.Any();
        }
    }
}
