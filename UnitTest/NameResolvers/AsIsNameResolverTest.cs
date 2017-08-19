using BenzeneSoft.QBuild.NameResolvers;
using NUnit.Framework;
using UnitTest.Doubles;

namespace UnitTest.NameResolvers
{
    [TestFixture]
    public class AsIsNameResolverTest
    {
        [Test]
        public void Resolve()
        {
            var resolver = new AsIsNameResolver();
            Assert.AreEqual(nameof(ProductTag), resolver.Resolve(typeof(ProductTag)));
            Assert.AreEqual(nameof(ProductTag.ProductId), resolver.Resolve(typeof(ProductTag).GetProperty("ProductId")));
        }
    }
}
