using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface IPredicateParser
    {
        ISql Parse<T>(Expression<Func<T, bool>> predicate);
    }
}