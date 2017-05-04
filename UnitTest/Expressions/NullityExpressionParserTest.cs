using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;
using static NUnit.Framework.Assert;

namespace UnitTest.Expressions
{
    [TestFixture]
    public class NullityExpressionParserTest
    {
        private NullityExpressionParser _parser;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
            _parser = new NullityExpressionParser(new ParserLookup(new LowerSnakeCaseNameResolver()));
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test]
        public void IsNull()
        {
            Expression<Predicate<Product>> exp = product => product.Comment == null;
            var predicate = _parser.Parse(exp.Body);
            var sql = new Clause("select * from product where ").Append(predicate).Append(" and name = 'bed'");

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }

        [Test]
        public void IsNotNull()
        {
            Expression<Predicate<Product>> exp = product => product.Comment != null;
            var predicate = _parser.Parse(exp.Body);
            var sql = new Clause("select * from product where ").Append(predicate);

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("chair", reader["name"]);
        }
    }
}
