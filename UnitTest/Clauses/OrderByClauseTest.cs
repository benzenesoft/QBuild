using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;
using UnitTest.Doubles;

namespace UnitTest.Clauses
{
    [TestFixture]
    public class OrderByClauseTest
    {
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
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
            var orderByClause = new OrderByClause().Asc("name");

            using (var reader = _connection.Read($"select * from product order by {orderByClause.Text}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
            }
        }

        [Test(Description = "select * from product order by name desc")]
        public void Desc_SingleColumn()
        {
            var orderByClause = new OrderByClause().Desc("name");

            using (var reader = _connection.Read($"select * from product order by {orderByClause.Text}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("table", reader["name"]);
            }
        }

        [Test(Description = "select * from product order by name asc, price asc")]
        public void Asc_MultiColumn()
        {
            var orderByClause = new OrderByClause().Asc("name").Asc("price");

            using (var reader = _connection.Read($"select * from product order by {orderByClause.Text}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(70, reader["price"]);

                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(80, reader["price"]);
            }
        }

        [Test(Description = "select * from product order by name desc, price desc")]
        public void Desc_MultiColumn()
        {
            var orderByClause = new OrderByClause().Desc("name").Desc("price");

            using (var reader = _connection.Read($"select * from product order by {orderByClause.Text}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("table", reader["name"]);
                Assert.AreEqual(55, reader["price"]);

                Assert.IsTrue(reader.Read());
                Assert.AreEqual("table", reader["name"]);
                Assert.AreEqual(40, reader["price"]);
            }
        }

        [Test(Description = "select * from product order by name asc, price desc")]
        public void Mix_MultiColumn()
        {
            var orderByClause = new OrderByClause().Asc("name").Desc("price");

            using (var reader = _connection.Read($"select * from product order by {orderByClause.Text}"))
            {
                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(80, reader["price"]);

                Assert.IsTrue(reader.Read());
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(70, reader["price"]);
            }
        }
    }
}
