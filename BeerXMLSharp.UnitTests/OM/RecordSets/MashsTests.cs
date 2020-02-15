using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class MashsTests
    {
        private Mock<Mash> GetMockMash()
        {
            Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

            steps.Setup(m => m.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            return new Mock<Mash>(
                70,
                steps,
                "Empty",
                1);
        }

        [TestMethod]
        public void Mashs_Valid_Empty()
        {
            Mashs Mashs = new Mashs();

            Assert.IsTrue(Mashs.IsValid());
        }

        [TestMethod]
        public void Mashs_Valid_NonEmpty()
        {
            Mashs Mashs = new Mashs();

            Mock<Mash> Mash = GetMockMash();

            Mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mashs.Add(Mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(Mashs.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Mash_Valid_ErrorCode()
        {
            Mashs Mashs = new Mashs();

            Mock<Mash> Mash = GetMockMash();

            Mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mashs.Add(Mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Mashs.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Mashs_Invalid_BadType()
        {
            Mashs Mashs = new Mashs();

            Mock<Mash> Mash = GetMockMash();

            Mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mashs.Add(Mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(Mashs.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Mashs_Invalid_BadType_ErrorCode()
        {
            Mashs Mashs = new Mashs();

            Mock<Mash> Mash = GetMockMash();

            Mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mashs.Add(Mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Mashs.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
