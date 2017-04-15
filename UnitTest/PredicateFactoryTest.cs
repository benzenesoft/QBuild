using System.Linq;
using BenzeneSoft.SqlBuilder;
using BenzeneSoft.SqlBuilder.Predicates;
using NUnit.Framework;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class PredicateFactoryTest
    {
        [Test]
        public void Expression()
        {
            var factory = new PredicateFactory<Product>(new LowerSnakeCaseNameResolver());

            var pred = factory.Expression(product => product.Name == "almira");

            Assert.AreEqual("almira", pred.Parameters.First().Value);
            Assert.AreEqual("name = @p0", pred.SqlText);
        }
    }
}
