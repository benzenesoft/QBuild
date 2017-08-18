using BenzeneSoft.QBuild.NameResolvers;
using NUnit.Framework;
using UnitTest.Doubles;

namespace UnitTest.NameResolvers
{
    [TestFixture]
    public class LowerSnameCaseNameResolverTest
    {
        [Test]
        public void CheckAll()
        {
            var resolver = new LowerSnakeCaseNameResolver();
            Assert.AreEqual("product_tag", resolver.Resolve(typeof(ProductTag)));

            Assert.AreEqual("product_id", resolver.Resolve(typeof(ProductTag).GetProperty("ProductId")));
        }
    }
}
