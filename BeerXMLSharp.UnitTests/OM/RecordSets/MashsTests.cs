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
            Mash_Steps steps = new Mash_Steps();

            return new Mock<Mash>(
                70.0,
                steps,
                "Empty",
                1);
        }

        [TestMethod]
        public void Mashs_Valid_Empty()
        {
            Mashs mashs = new Mashs();

            Assert.IsTrue(mashs.IsValid());
        }

        [TestMethod]
        public void Mashs_Valid_NonEmpty()
        {
            Mashs mashs = new Mashs();

            Mock<Mash> mash = GetMockMash();

            mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mashs.Add(mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(mashs.IsValidRecordSet(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Mash_Valid_ErrorCode()
        {
            Mashs mashs = new Mashs();

            Mock<Mash> mash = GetMockMash();

            mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mashs.Add(mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            mashs.IsValidRecordSet(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Mashs_Invalid_BadType()
        {
            Mashs mashs = new Mashs();

            Mock<Mash> mash = GetMockMash();

            mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mashs.Add(mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(mashs.IsValidRecordSet(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Mashs_Invalid_BadType_ErrorCode()
        {
            Mashs mashs = new Mashs();

            Mock<Mash> mash = GetMockMash();

            mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mashs.Add(mash.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            mashs.IsValidRecordSet(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
