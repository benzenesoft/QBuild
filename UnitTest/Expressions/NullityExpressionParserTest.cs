using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.NameResolvers;
using NUnit.Framework;
using UnitTest.Doubles;
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
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate).Append(" and name = 'bed'");

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }

        [Test]
        public void IsNotNull()
        {
            Expression<Predicate<Product>> exp = product => product.Comment != null;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate);

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("chair", reader["name"]);
        }
    }
}
