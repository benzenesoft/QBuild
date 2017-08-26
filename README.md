# QBuild
A C# library to easily build parameterized SQL queries

# What it supports
1. Building parameterized queries
1. Lambda expressions
1. Custom column/table name mapping
1. Custom function mapping

# What QBuild is NOT for
1. Executing queries, use plain `IDbCommand.Execute*` methods or something else.
1. Object mapping. Use something like [Dapper](https://github.com/StackExchange/Dapper)

# Code snippets
``` csharp
LambdaQueryBuilder builder = new LambdaQueryBuilder();
IClause clause = builder
    .Select<Product>(product => product.Name)
    .From<Product>()
    .OrderByDesc<Product>(product => product.Name)
    .Build();
```

Produces
``` SQL
select Name from Product order by Name desc
```

More use cases can be found in UnitTest project


