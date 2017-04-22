using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public abstract class TypedExpressionParser<TExpression> : IExpressionParser 
        where TExpression : Expression
    {
        protected abstract ISql ParseTyped(TExpression expression);

        public virtual ISql Parse(Expression expression)
        {
            if (!(expression is TExpression))
            {
                throw new ArgumentException($"expression must be instance of {typeof(TExpression)}", nameof(expression));
            }

            return ParseTyped((TExpression)expression);
        }
    }
}