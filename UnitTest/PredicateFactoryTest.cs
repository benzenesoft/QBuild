using System.Linq;
using BenzeneSoft.SqlBuilder;
using BenzeneSoft.SqlBuilder.Predicates;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class PredicateFactoryTest
    {
        private PredicateFactory<Product> _factory;
        private Sql _sql;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
            _factory = new PredicateFactory<Product>(new LowerSnakeCaseNameResolver());
            _sql = new Sql("select * from product WHERE ");
            _connection = new TestConnection();
            _connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Dispose();
        }

        [Test]
        public void Expression_Equal()
        {
            var pred = _factory.Expression(product => product.Name == "almira");
            _sql.Append(pred);

            var reader = _connection.Read(_sql);

            Assert.IsTrue(reader.Read());
            Assert.AreEqual("almira", reader["name"]);

            Assert.IsTrue(reader.Read());
            Assert.AreEqual("almira", reader["name"]);

            Assert.IsFalse(reader.Read());
        }
    }
}
