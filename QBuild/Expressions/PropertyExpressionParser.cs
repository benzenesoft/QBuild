﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PropertyExpressionParser : TypedExpressionParser<MemberExpression>
    {
        private readonly INameResolver _nameResolver;

        public PropertyExpressionParser(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        protected override ISql ParseTyped(MemberExpression expression)
        {
            var prop = expression.Member as PropertyInfo;
            if (prop == null)
            {
                throw new ArgumentException("member expression must be a property", nameof(expression));
            }

            var column = _nameResolver.Column(prop);
            return new Sql(column);
        }
    }
}