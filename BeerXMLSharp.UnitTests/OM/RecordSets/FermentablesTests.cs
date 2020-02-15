using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class FermentablesTests
    {
        private Mock<Fermentable> GetMockFermentable()
        {
            return new Mock<Fermentable>(
                fermentableName,
                type,
                amount,
                yield,
                color);
        }

        [TestMethod]
        public void Fermentables_Valid_Empty()
        {
            Fermentables Fermentables = new Fermentables();

            Assert.IsTrue(Fermentables.IsValid());
        }

        [TestMethod]
        public void Fermentables_Valid_NonEmpty()
        {
            Fermentables Fermentables = new Fermentables();

            Mock<Fermentable> Fermentable = GetMockFermentable();

            Fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Fermentables.Add(Fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(Fermentables.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Fermentable_Valid_ErrorCode()
        {
            Fermentables Fermentables = new Fermentables();

            Mock<Fermentable> Fermentable = GetMockFermentable();

            Fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Fermentables.Add(Fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Fermentables.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Fermentables_Invalid_BadType()
        {
            Fermentables Fermentables = new Fermentables();

            Mock<Fermentable> Fermentable = GetMockFermentable();

            Fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Fermentables.Add(Fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(Fermentables.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Fermentables_Invalid_BadType_ErrorCode()
        {
            Fermentables Fermentables = new Fermentables();

            Mock<Fermentable> Fermentable = GetMockFermentable();

            Fermentable.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Fermentables.Add(Fermentable.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Fermentables.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
