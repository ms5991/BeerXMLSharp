using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class FermentablesTests
    {
        private Mock<Fermentable> GetMockFermentable()
        {
            return new Mock<Fermentable>(
                "Test",
                FermentableType.Adjunct,
                4.5,
                5,
                5,
                1);
        }

        [TestMethod]
        public void Fermentables_Valid_Empty()
        {
            Fermentables fermentables = new Fermentables();

            Assert.IsTrue(fermentables.IsValid());
        }

        [TestMethod]
        public void Fermentables_Valid_NonEmpty()
        {
            Fermentables fermentables = new Fermentables();

            Mock<Fermentable> fermentable = GetMockFermentable();

            fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            fermentables.Add(fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(fermentables.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Fermentable_Valid_ErrorCode()
        {
            Fermentables fermentables = new Fermentables();

            Mock<Fermentable> fermentable = GetMockFermentable();

            fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            fermentables.Add(fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            fermentables.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Fermentables_Invalid_BadType()
        {
            Fermentables fermentables = new Fermentables();

            Mock<Fermentable> fermentable = GetMockFermentable();

            fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            fermentables.Add(fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(fermentables.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Fermentables_Invalid_BadType_ErrorCode()
        {
            Fermentables fermentables = new Fermentables();

            Mock<Fermentable> fermentable = GetMockFermentable();

            fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            fermentables.Add(fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            fermentables.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
