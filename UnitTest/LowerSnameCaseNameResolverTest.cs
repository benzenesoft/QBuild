﻿using BenzeneSoft.SqlBuilder;
using NUnit.Framework;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class LowerSnameCaseNameResolverTest
    {
        [Test]
        public void CheckAll()
        {
            var resolver = new LowerSnakeCaseNameResolver();
            Assert.AreEqual("product_tag", resolver.Table(typeof(ProductTag)));

            Assert.AreEqual("product_id", resolver.Column<ProductTag>(pt => pt.ProductId));
        }
    }
}