using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class HopTests
    {
        [TestMethod]
        public void Hop_Valid()
        {
            Hop hop = new Hop(
                "Citra",
                1.2,
                1.2,
                HopUse.Aroma,
                2);

            Assert.IsTrue(hop.IsValid());
        }
    }
}
