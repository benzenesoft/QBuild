﻿using BenzeneSoft.QBuild.Builders;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.NameResolvers;
using NUnit.Framework;
using UnitTest.Doubles;
using static BenzeneSoft.QBuild.Builders.ClauseFactory;

namespace UnitTest.Builders
{
    [TestFixture]
    public class QueryBuilderWithClauseFactoryTest
    {
        private RawQueryBuilder _builder;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
            _builder = new RawQueryBuilder();
            SetDefaultParser(new LambdaParser(new ParserLookup(new LowerSnakeCaseNameResolver())));
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test(Description = "select * from product where id = 1")]
        public void SimpleSelect()
        {
            var query = _builder
                .Select(Select().All())
                .From(From().Table("product"))
                .Where(Where<Product>(product => product.Id == 1))
                .Build();

            var reader = _connection.Read(query);
            Assert.IsTrue(reader.Read());
            Assert.AreEqual(1, reader["id"]);
            Assert.AreEqual("almira", reader["name"]);
            Assert.AreEqual(80, reader["price"]);
            Assert.IsFalse(reader.Read());
        }
    }
}
