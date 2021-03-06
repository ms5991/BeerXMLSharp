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
            Miscs miscs = new Miscs();

            Assert.IsTrue(miscs.IsValid());
        }

        [TestMethod]
        public void Miscs_Valid_NonEmpty()
        {
            Miscs miscs = new Miscs();

            Mock<Misc> misc = GetMockMisc();

            misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            miscs.Add(misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(miscs.IsValidRecordSet(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Misc_Valid_ErrorCode()
        {
            Miscs miscs = new Miscs();

            Mock<Misc> misc = GetMockMisc();

            misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            miscs.Add(misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            miscs.IsValidRecordSet(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Miscs_Invalid_BadType()
        {
            Miscs miscs = new Miscs();

            Mock<Misc> misc = GetMockMisc();

            misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            miscs.Add(misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(miscs.IsValidRecordSet(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Miscs_Invalid_BadType_ErrorCode()
        {
            Miscs miscs = new Miscs();

            Mock<Misc> misc = GetMockMisc();

            misc.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            miscs.Add(misc.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            miscs.IsValidRecordSet(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
