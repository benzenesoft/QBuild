using System;

namespace BenzeneSoft.SqlBuilder
{
    public interface IPredicateFactory
    {
        ISql Binary(string leftExpression, string comparison, object rightValue);
        ISql Binary(string leftExpression, string comparison, string rightExpression);
    }

    public interface IPredicateFactory<out T>
    {
        ISql Binary(Func<T, object> leftExpression, string comparison, object rightValue);
        ISql Binary(Func<T, object> leftExpression, string comparison, Func<T, object> rightExpression);
    }
}