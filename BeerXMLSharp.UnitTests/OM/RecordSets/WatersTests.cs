using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class WatersTests
    {
        private Mock<Water> GetMockWater()
        {
            return new Mock<Water>(
                5,
                5,
                5,
                5,
                5,
                5,
                5,
                "Water",
                1);
        }

        [TestMethod]
        public void Waters_Valid_Empty()
        {
            Waters waters = new Waters();

            Assert.IsTrue(waters.IsValid());
        }

        [TestMethod]
        public void Waters_Valid_NonEmpty()
        {
            Waters waters = new Waters();

            Mock<Water> water = GetMockWater();

            water.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            waters.Add(water.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(waters.IsValidRecordSet(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Water_Valid_ErrorCode()
        {
            Waters waters = new Waters();

            Mock<Water> water = GetMockWater();

            water.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            waters.Add(water.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            waters.IsValidRecordSet(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Waters_Invalid_BadType()
        {
            Waters waters = new Waters();

            Mock<Water> water = GetMockWater();

            water.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            waters.Add(water.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(waters.IsValidRecordSet(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Waters_Invalid_BadType_ErrorCode()
        {
            Waters waters = new Waters();

            Mock<Water> water = GetMockWater();

            water.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            waters.Add(water.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            waters.IsValidRecordSet(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
