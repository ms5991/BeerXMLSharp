using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using BeerXMLSharp.OM;
using Moq;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class Mash_StepsTests
    {
        private Mock<Mash_Step> GetMockMash_Step()
        {
            return new Mock<Mash_Step>(
                MashStepType.Infusion,
                1.0,
                1.0,
                "Step 1",
                1);
        }

        [TestMethod]
        public void Mash_Steps_Valid_Empty()
        {
            Mash_Steps Mash_Steps = new Mash_Steps();

            Assert.IsTrue(Mash_Steps.IsValid());
        }

        [TestMethod]
        public void Mash_Steps_Valid_NonEmpty()
        {
            Mash_Steps Mash_Steps = new Mash_Steps();

            Mock<Mash_Step> Mash_Step = GetMockMash_Step();

            Mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mash_Steps.Add(Mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(Mash_Steps.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Mash_Step_Valid_ErrorCode()
        {
            Mash_Steps Mash_Steps = new Mash_Steps();

            Mock<Mash_Step> Mash_Step = GetMockMash_Step();

            Mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mash_Steps.Add(Mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Mash_Steps.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Mash_Steps_Invalid_BadType()
        {
            Mash_Steps Mash_Steps = new Mash_Steps();

            Mock<Mash_Step> Mash_Step = GetMockMash_Step();

            Mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mash_Steps.Add(Mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(Mash_Steps.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Mash_Steps_Invalid_BadType_ErrorCode()
        {
            Mash_Steps Mash_Steps = new Mash_Steps();

            Mock<Mash_Step> Mash_Step = GetMockMash_Step();

            Mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mash_Steps.Add(Mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Mash_Steps.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
