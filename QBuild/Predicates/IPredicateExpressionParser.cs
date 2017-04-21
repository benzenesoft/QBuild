using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Predicates
{
    public interface IPredicateExpressionParser
    {
        ISql Parse<T>(Expression<Predicate<T>> expression);
    }
}
