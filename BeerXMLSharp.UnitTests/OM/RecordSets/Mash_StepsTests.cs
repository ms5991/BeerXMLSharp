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
            Mash_Steps mash_Steps = new Mash_Steps();

            Assert.IsTrue(mash_Steps.IsValid());
        }

        [TestMethod]
        public void Mash_Steps_Valid_NonEmpty()
        {
            Mash_Steps mash_Steps = new Mash_Steps();

            Mock<Mash_Step> mash_Step = GetMockMash_Step();

            mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mash_Steps.Add(mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(mash_Steps.IsValidRecordSet(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Mash_Step_Valid_ErrorCode()
        {
            Mash_Steps mash_Steps = new Mash_Steps();

            Mock<Mash_Step> mash_Step = GetMockMash_Step();

            mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mash_Steps.Add(mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            mash_Steps.IsValidRecordSet(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Mash_Steps_Invalid_BadType()
        {
            Mash_Steps mash_Steps = new Mash_Steps();

            Mock<Mash_Step> mash_Step = GetMockMash_Step();

            mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mash_Steps.Add(mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(mash_Steps.IsValidRecordSet(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Mash_Steps_Invalid_BadType_ErrorCode()
        {
            Mash_Steps mash_Steps = new Mash_Steps();

            Mock<Mash_Step> mash_Step = GetMockMash_Step();

            mash_Step.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            mash_Steps.Add(mash_Step.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            mash_Steps.IsValidRecordSet(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
