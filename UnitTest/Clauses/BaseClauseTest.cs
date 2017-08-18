using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace UnitTest.Clauses
{
    internal sealed class BaseClauseForTest : BaseClause
    {
        public override IEnumerable<Parameter> Parameters { get; }
        public override string Text { get; }

        public BaseClauseForTest(string text, params Parameter[] parameters)
        {
            Text = text;
            Parameters = parameters;
        }
    }

    [TestFixture]
    public class BaseClauseTest
    {
        [Test]
        public void ToString_Test()
        {
            var c1 = new BaseClauseForTest("color = @p1", Parameter.CreateNew("@p1", "red"));
            Assert.AreEqual("color = @p1", c1.ToString());
        }
        
        [Test]
        public void IsEmpty_Empty_GivesTrue()
        {
            var c1 = new BaseClauseForTest("");
            Assert.IsTrue(c1.IsEmpty);
        }

        [Test]
        public void IsEmpty_Null_GivesTrue()
        {
            var c1 = new BaseClauseForTest(null);
            Assert.IsTrue(c1.IsEmpty);
        }
        
        [Test]
        public void IsEmpty_NotEmpty_GivesFalse()
        {
            var c1 = new BaseClauseForTest("color = @p1", Parameter.CreateNew("@p1", "red"));
            Assert.IsFalse(c1.IsEmpty);
        }

        [Test]
        public void IsEmpty_WhiteSpace_GivesFalse()
        {
            var c1 = new BaseClauseForTest(" ");
            Assert.IsFalse(c1.IsEmpty);
        }

        [Test]
        public void IsEmpty_TextEmptyButHasParams_GivesFalse()
        {
            var c1 = new BaseClauseForTest("", Parameter.CreateNew(23));
            Assert.IsFalse(c1.IsEmpty);
        }

        [Test]
        public void Plus()
        {
            var c1 = new BaseClauseForTest("color = @p1", Parameter.CreateNew("@p1", "red"));
            var c2 = new BaseClauseForTest(" and price < @p2", new Parameter("@p2", 100));

            var c = c1 + c2;

            Assert.AreEqual("color = @p1 and price < @p2", c.Text);

            var paramsArray = c.Parameters.ToArray();
            Assert.AreEqual("@p1", paramsArray[0].Name);
            Assert.AreEqual("@p2", paramsArray[1].Name);
        }

        [Test]
        public void Or()
        {
            var c1 = new BaseClauseForTest("color = @p1", Parameter.CreateNew("@p1", "red"));
            var c2 = new BaseClauseForTest("price < @p2", new Parameter("@p2", 100));

            var c = c1 | c2;

            Assert.AreEqual("(color = @p1) OR (price < @p2)", c.Text);

            var paramsArray = c.Parameters.ToArray();
            Assert.AreEqual("@p1", paramsArray[0].Name);
            Assert.AreEqual("@p2", paramsArray[1].Name);
        }

        [Test]
        public void And()
        {
            var c1 = new BaseClauseForTest("color = @p1", Parameter.CreateNew("@p1", "red"));
            var c2 = new BaseClauseForTest("price < @p2", new Parameter("@p2", 100));

            var c = c1 & c2;

            Assert.AreEqual("(color = @p1) AND (price < @p2)", c.Text);

            var paramsArray = c.Parameters.ToArray();
            Assert.AreEqual("@p1", paramsArray[0].Name);
            Assert.AreEqual("@p2", paramsArray[1].Name);
        }
    }
}
