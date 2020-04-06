using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vanme_Pro_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int x = 3;
            int y = 4;

            var z = x + y;

            Assert.AreEqual(z, 7);
        }
    }
}
