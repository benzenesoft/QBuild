using BenzeneSoft.SqlBuilder;
using NUnit.Framework;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class FromBuilderTest
    {
        [Test]
        public void FromString()
        {
            var fromBuilder = new FromBuilder<Product>(new LowerSnakeCaseNameResolver());
            
            Assert.AreEqual("FROM product_table", fromBuilder.From("product_table").Build().SqlText);
        }

        [Test]
        public void Default_FromTypeArgument()
        {
            var fromBuilder = new FromBuilder<Product>(new LowerSnakeCaseNameResolver());

            Assert.AreEqual("FROM product", fromBuilder.Default().Build().SqlText);
        }
    }
}