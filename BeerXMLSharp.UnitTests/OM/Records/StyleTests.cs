using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class StyleTests
    {
        [TestMethod]
        public void Style_Valid()
        {
            Style style = new Style(
                "Category",
                "1",
                "A",
                "Guide",
                StyleType.Ale,
                1.06,
                1.05,
                60.0,
                70.0,
                70.0,
                70.0,
                "Style",
                1);

            Assert.IsTrue(style.IsValid());
        }

        [TestMethod]
        public void Style_Valid_ErrorCode()
        {
            Style style = new Style(
                "Category",
                "1",
                "A",
                "Guide",
                StyleType.Ale,
                1.06,
                1.05,
                60.0,
                70.0,
                70.0,
                70.0,
                "Style",
                1);

            ValidationCode errorCode = ValidationCode.SUCCESS;
            style.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }
    }
}
