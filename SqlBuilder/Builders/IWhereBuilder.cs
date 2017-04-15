﻿namespace BenzeneSoft.SqlBuilder.Builders
{
    public interface IWhereBuilder : ISqlBuilder
    {
        IWhereBuilder Or(ISql predicate);
        IWhereBuilder And(ISql predicate);
        IWhereBuilder Begin(ISql predicate);
    }
}