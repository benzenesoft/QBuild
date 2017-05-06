﻿using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public static class ClauseFactory
    {
        private static ILambdaParser _defaultParser;

        public static SelectClause Select()
        {
            return new SelectClause();
        }

        public static FromClause From()
        {
            return new FromClause();
        }

        public static IClause Where<T>(Expression<Predicate<T>> predicate)
        {
            return Lambda().Expression(predicate);
        }

        public static GroupByClause GroupBy()
        {
            return new GroupByClause();
        }

        public static OrderByClause OrderBy()
        {
            return new OrderByClause();
        }
        
        public static LambdaClause Lambda()
        {
            return Lambda(_defaultParser);
        }

        public static LambdaClause Lambda(ILambdaParser parser)
        {
            return new LambdaClause(parser);
        }

        public static void SetDefaultParser(ILambdaParser parser)
        {
            _defaultParser = parser;
        }
    }
}