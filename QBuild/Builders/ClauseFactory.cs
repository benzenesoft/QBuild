using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.NameResolvers;

namespace BenzeneSoft.QBuild.Builders
{
    public static class ClauseFactory
    {
        private static ILambdaResolver _defaultParser = new LambdaResolver(new ParserLookup(new AsIsNameResolver(), new SqlFunctionNameResolver(new AsIsNameResolver())));

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
            return Lambda().Expression(predicate, ClauseContext.Where);
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

        public static LambdaClause Lambda(ILambdaResolver parser)
        {
            return new LambdaClause(parser);
        }

        public static void SetDefaultParser(ILambdaResolver parser)
        {
            _defaultParser = parser;
        }
    }
}
