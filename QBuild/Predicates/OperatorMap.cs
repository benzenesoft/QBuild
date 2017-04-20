﻿using System.Collections.Generic;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Predicates
{
    public class OperatorMap
    {
        private readonly Dictionary<ExpressionType, string> _map = new Dictionary<ExpressionType, string>
        {
            {ExpressionType.Equal, "=" },
            {ExpressionType.GreaterThan, ">" },
            {ExpressionType.GreaterThanOrEqual, ">=" },
            {ExpressionType.LessThan, "<" },
            {ExpressionType.LessThanOrEqual, "<=" }
        };

        public string this[ExpressionType expressionType] => _map[expressionType];
    }
}