using BenzeneSoft.SqlBuilder;
using BenzeneSoft.SqlBuilder.Builders;
using NUnit.Framework;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class SelectBuilderTest
    {
        [Test]
        public void All()
        {
            var sb = new SelectBuilder<Product>(new LowerSnakeCaseNameResolver());

            var sql = sb.All().Build();
            Assert.AreEqual("SELECT *", sql.SqlText);
        }

        [Test]
        public void Column_String()
        {
            var sb = new SelectBuilder<Product>(new LowerSnakeCaseNameResolver());

            var sql = sb.Columns("id", "name").Build();
            Assert.AreEqual("SELECT id\n,name", sql.SqlText);
        }

        [Test]
        public void Column_Expression()
        {
            var sb = new SelectBuilder<Product>(new LowerSnakeCaseNameResolver());
            
            var sql = sb.Columns(p => p.Id, p => p.Name).Build();
            Assert.AreEqual("SELECT id\n,name", sql.SqlText);
        }

        [Test]
        public void Column_Custom_ReplacesOthers()
        {
            var sb = new SelectBuilder<Product>(new LowerSnakeCaseNameResolver());
            
            var sql = sb.Columns(p => p.Id, p => p.Name).Columns("is_in_stock").Custom(new Sql("whatever")).Build();
            Assert.AreEqual("whatever", sql.SqlText);
        }
    }
}
