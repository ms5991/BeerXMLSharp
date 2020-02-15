using BeerXMLSharp.OM;
using BeerXMLSharp.OM.RecordSets;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class StylesTests
    {
        private Mock<Style> GetMockStyle()
        {
            return new Mock<Style>(
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
        }

        [TestMethod]
        public void Styles_Valid_Empty()
        {
            Styles Styles = new Styles();

            Assert.IsTrue(Styles.IsValid());
        }

        [TestMethod]
        public void Styles_Valid_NonEmpty()
        {
            Styles Styles = new Styles();

            Mock<Style> Style = GetMockStyle();

            Style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Styles.Add(Style.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(Styles.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Style_Valid_ErrorCode()
        {
            Styles Styles = new Styles();

            Mock<Style> Style = GetMockStyle();

            Style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Styles.Add(Style.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Styles.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Styles_Invalid_BadType()
        {
            Styles Styles = new Styles();

            Mock<Style> Style = GetMockStyle();

            Style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Styles.Add(Style.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(Styles.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Styles_Invalid_BadType_ErrorCode()
        {
            Styles Styles = new Styles();

            Mock<Style> Style = GetMockStyle();

            Style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Styles.Add(Style.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Styles.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
