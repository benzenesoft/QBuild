using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class OrderByBuilderTest
    {
        private TestConnection _connection;
        private OrderByBuilder<Product> _orderByBuilder;

        [SetUp]
        public void Setup()
        {
            var nameResolver = new LowerSnakeCaseNameResolver();
            _orderByBuilder = new OrderByBuilder<Product>(nameResolver);

            _connection = new TestConnection();
            _connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
            _connection.Dispose();
        }

        [Test(Description = "select * from product order by name asc")]
        public void Asc_SingleColumn()
        {
            var orderSql = _orderByBuilder.Asc(product => product.Name).Build();

            using (var reader = _connection.Read($"select * from product {orderSql.SqlText}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
            }
        }

        [Test(Description = "select * from product order by name desc")]
        public void Desc_SingleColumn()
        {
            var orderSql = _orderByBuilder.Desc(product => product.Name).Build();

            using (var reader = _connection.Read($"select * from product {orderSql.SqlText}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("zipper", reader["name"]);
            }
        }

        [Test(Description = "select * from product order by name asc, price asc")]
        public void Asc_MultiColumn()
        {
            var orderSql = _orderByBuilder.Asc(product => product.Name, product => product.Price).Build();

            using (var reader = _connection.Read($"select * from product {orderSql.SqlText}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(500, reader["price"]);

                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(1000, reader["price"]);
            }
        }

        [Test(Description = "select * from product order by name desc, price desc")]
        public void Desc_MultiColumn()
        {
            var orderSql = _orderByBuilder.Desc(product => product.Name, product => product.Price).Build();

            using (var reader = _connection.Read($"select * from product {orderSql.SqlText}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("zipper", reader["name"]);
                Assert.AreEqual(10, reader["price"]);

                Assert.IsTrue(reader.Read());
                Assert.AreEqual("zipper", reader["name"]);
                Assert.AreEqual(5, reader["price"]);
            }
        }

        [Test(Description = "select * from product order by name asc, price desc")]
        public void Mix_MultiColumn()
        {
            var orderSql = _orderByBuilder.Asc(product => product.Name).Desc(product => product.Price).Build();

            using (var reader = _connection.Read($"select * from product {orderSql.SqlText}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(1000, reader["price"]);

                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(500, reader["price"]);
            }
        }
    }
}
