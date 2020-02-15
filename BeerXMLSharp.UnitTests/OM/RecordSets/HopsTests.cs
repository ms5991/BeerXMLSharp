using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class HopsTests
    {

        private Mock<Hop> GetMockHop()
        {
            return new Mock<Hop>(
                "Citra",
                1.2,
                1.2,
                HopUse.Aroma,
                2,
                1);
        }

        [TestMethod]
        public void Hops_Valid_Empty()
        {
            Hops hops = new Hops();

            Assert.IsTrue(hops.IsValid());
        }

        [TestMethod]
        public void Hops_Valid_NonEmpty()
        {
            Hops hops = new Hops();

            Mock<Hop> hop = GetMockHop();

            hop.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            hops.Add(hop.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(hops.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Hop_Valid_ErrorCode()
        {
            Hops hops = new Hops();

            Mock<Hop> hop = GetMockHop();

            hop.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            hops.Add(hop.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            hops.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Hops_Invalid_BadType()
        {
            Hops hops = new Hops();

            Mock<Hop> hop = GetMockHop();

            hop.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            hops.Add(hop.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(hops.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Hops_Invalid_BadType_ErrorCode()
        {
            Hops hops = new Hops();

            Mock<Hop> hop = GetMockHop();

            hop.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            hops.Add(hop.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            hops.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
