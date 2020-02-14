using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class Mash_StepTests
    {
        [TestMethod]
        public void MashStep_Valid()
        {
            Mash_Step steps = new Mash_Step(
                MashStepType.Infusion,
                1.0,
                1.0,
                "Step 1");

            Assert.IsTrue(steps.IsValid());
        }

        [TestMethod]
        public void MashStep_Decotion_NonNullInfusion_Invalid()
        {
            Mash_Step steps = new Mash_Step(
                MashStepType.Decoction,
                1.0,
                1.0,
                "Step 1");

            steps.Infuse_Amount = 1.0;

            Assert.IsFalse(steps.IsValid());
        }

        [TestMethod]
        public void MashStep_Decotion_NonNullInfusion_Invalid_ErrorCode()
        {
            Mash_Step steps = new Mash_Step(
                MashStepType.Decoction,
                1.0,
                1.0,
                "Step 1");

            steps.Infuse_Amount = 1.0;

            ValidationCode errorCode = ValidationCode.SUCCESS;

            steps.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.DECOCTION_NON_EMPTY_INFUSE_AMOUNT, errorCode);
        }

        [TestMethod]
        public void MashStep_Infusion_NonNullInfusion_Valid()
        {
            Mash_Step steps = new Mash_Step(
                MashStepType.Infusion,
                1.0,
                1.0,
                "Step 1");

            steps.Infuse_Amount = 1.0;

            Assert.IsTrue(steps.IsValid());
        }

        [TestMethod]
        public void MashStep_Temperature_NonNullInfusion_Valid()
        {
            Mash_Step steps = new Mash_Step(
                MashStepType.Temperature,
                1.0,
                1.0,
                "Step 1");

            steps.Infuse_Amount = 1.0;

            Assert.IsTrue(steps.IsValid());
        }
    }
}
