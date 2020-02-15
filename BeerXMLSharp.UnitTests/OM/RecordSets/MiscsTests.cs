using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class MiscsTests
    {
        private Mock<Misc> GetMockMisc()
        {
            return new Mock<Misc>(
                MiscType.Fining,
                MiscUse.Boil,
                1.0,
                1.0,
                "Test",
                false,
                1);
        }

        [TestMethod]
        public void Miscs_Valid_Empty()
        {
            Miscs Miscs = new Miscs();

            Assert.IsTrue(Miscs.IsValid());
        }

        [TestMethod]
        public void Miscs_Valid_NonEmpty()
        {
            Miscs Miscs = new Miscs();

            Mock<Misc> Misc = GetMockMisc();

            Misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Miscs.Add(Misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(Miscs.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Misc_Valid_ErrorCode()
        {
            Miscs Miscs = new Miscs();

            Mock<Misc> Misc = GetMockMisc();

            Misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Miscs.Add(Misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Miscs.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Miscs_Invalid_BadType()
        {
            Miscs Miscs = new Miscs();

            Mock<Misc> Misc = GetMockMisc();

            Misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Miscs.Add(Misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(Miscs.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Miscs_Invalid_BadType_ErrorCode()
        {
            Miscs Miscs = new Miscs();

            Mock<Misc> Misc = GetMockMisc();

            Misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Miscs.Add(Misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Miscs.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
