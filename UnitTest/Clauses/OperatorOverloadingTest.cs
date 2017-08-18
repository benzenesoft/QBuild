using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;
using System.Linq;

namespace UnitTest.Clauses
{
    [TestFixture]
    public class OperatorOverloadingTest
    {
        [Test]
        public void Plus()
        {
            var c1 = new Clause("select * from products where price > @p1", Parameter.CreateNew("@p1", 50));
            var c2 = new Clause(" and price < @p2", new Parameter("@p2", 100));

            var c = c1 + c2;

            Assert.AreEqual("select * from products where price > @p1 and price < @p2", c.Text);

            var paramsArray = c.Parameters.ToArray();
            Assert.AreEqual("@p1", paramsArray[0].Name);
            Assert.AreEqual("@p2", paramsArray[1].Name);
        }
    }
}
