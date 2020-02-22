using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class YeastsTests
    {
        private Mock<Yeast> GetMockYeast()
        {
            return new Mock<Yeast>(
                   YeastType.Ale,
                   YeastForm.Culture,
                   3,
                   "Test",
                   false,
                   1);
        }

        [TestMethod]
        public void Yeasts_Valid_Empty()
        {
            Yeasts yeasts = new Yeasts();

            Assert.IsTrue(yeasts.IsValid());
        }

        [TestMethod]
        public void Yeasts_Valid_NonEmpty()
        {
            Yeasts yeasts = new Yeasts();

            Mock<Yeast> yeast = GetMockYeast();

            yeast.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            yeasts.Add(yeast.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(yeasts.IsValidRecordSet(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Yeast_Valid_ErrorCode()
        {
            Yeasts yeasts = new Yeasts();

            Mock<Yeast> yeast = GetMockYeast();

            yeast.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            yeasts.Add(yeast.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            yeasts.IsValidRecordSet(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Yeasts_Invalid_BadType()
        {
            Yeasts yeasts = new Yeasts();

            Mock<Yeast> yeast = GetMockYeast();

            yeast.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            yeasts.Add(yeast.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(yeasts.IsValidRecordSet(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Yeasts_Invalid_BadType_ErrorCode()
        {
            Yeasts yeasts = new Yeasts();

            Mock<Yeast> yeast = GetMockYeast();

            yeast.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            yeasts.Add(yeast.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            yeasts.IsValidRecordSet(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
