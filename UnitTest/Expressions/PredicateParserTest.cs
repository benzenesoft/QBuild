using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Expressions;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;
using static NUnit.Framework.Assert;

namespace UnitTest.Expressions
{
    [TestFixture]
    public class PredicateParserTest
    {
        private PredicateParser _parser;
        private TestConnection _connection;
        [SetUp]
        public void Setup()
        {
            _parser = new PredicateParser(new LowerSnakeCaseNameResolver(), new OperatorResolver());
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test]
        public void Constant()
        {
            var predicate = _parser.Parse<Product>(product => 1 == 1);
            var sql = new Sql("select * from product where ").Append(predicate).Append(" order by name");

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("almira", reader["name"]);
        }

        [Test]
        public void SimpleBinary()
        {
            var predicate = _parser.Parse<Product>(product => product.Name == "bed");
            var sql = new Sql("select * from product where ").Append(predicate).Append(" order by name");

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }

        [Test]
        public void SimpleBoolean_True()
        {
            var predicate = _parser.Parse<Product>(product => product.IsAvailable);
            var sql = new Sql("select * from product where ").Append(predicate).Append(" order by name");

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }

        [Test]
        public void SimpleBoolean_False()
        {
            var predicate = _parser.Parse<Product>(product => !product.IsAvailable);
            var sql = new Sql("select * from product where ").Append(predicate).Append(" order by name");

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("almira", reader["name"]);
        }
    }
}
