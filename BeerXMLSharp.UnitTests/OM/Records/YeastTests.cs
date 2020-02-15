using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class YeastTests
    {
        [TestMethod]
        public void Yeast_Valid()
        {
            Yeast yeast = new Yeast(
                YeastType.Ale,
                YeastForm.Culture,
                3,
                "Test");

            Assert.IsTrue(yeast.IsValid());
        }

        [TestMethod]
        public void Yeast_Valid_ErrorCode()
        {
            Yeast yeast = new Yeast(
                YeastType.Ale,
                YeastForm.Culture,
                3,
                "Test");

            ValidationCode errorCode = ValidationCode.SUCCESS;

            yeast.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }
    }
}
