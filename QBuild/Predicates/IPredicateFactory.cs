using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Predicates
{
    public interface IPredicateFactory
    {
        ISql Binary(string leftExpression, string comparison, object rightValue);
        ISql Binary(string leftExpression, string comparison, string rightExpression);
        ISql Or(params ISql[] expressions);
        ISql And(params ISql[] expressions);
    }

    public interface IPredicateFactory<T> : IPredicateFactory
    {
        ISql Binary(Expression<Func<T, object>> leftExpression, string comparison, object rightValue);
        ISql Binary(Expression<Func<T, object>> leftExpression, string comparison, Expression<Func<T, object>> rightExpression);
        ISql Expression(Expression<Predicate<T>> expression);
    }
}