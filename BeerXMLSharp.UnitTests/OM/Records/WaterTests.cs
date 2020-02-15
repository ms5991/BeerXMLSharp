using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class WaterTests
    {
        [TestMethod]
        public void Water_Valid()
        {
            Water water = new Water(
                5,
                5,
                5,
                5,
                5,
                5,
                5,
                "Water");

            Assert.IsTrue(water.IsValid());
        }

        [TestMethod]
        public void Water_Valid_ErrorCode()
        {
            Water water = new Water(
                5,
                5,
                5,
                5,
                5,
                5,
                5,
                "Water");

            ValidationCode errorCode = ValidationCode.SUCCESS;

            water.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }
    }
}
