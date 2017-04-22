using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public abstract class ExpressionParser<TExpression> : IExpressionParser 
        where TExpression : Expression
    {
        protected abstract ISql ParseImpl(TExpression expression);

        public virtual ISql Parse(Expression expression)
        {
            if (!(expression is TExpression))
            {
                throw new ArgumentException($"expression must be instance of {typeof(TExpression)}", nameof(expression));
            }

            return ParseImpl((TExpression)expression);
        }
    }
}