using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class MashTests
    {
        [TestMethod]
        public void Mash_EmptySteps_Valid()
        {
            Mash_Steps steps = new Mash_Steps();

            Mash mash = new Mash(
                70,
                steps,
                "Empty");

            Assert.IsTrue(mash.IsValid());
        }

        [TestMethod]
        public void Mash_InvalidMashSteps_Invalid()
        {
            Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

            steps.Setup(m => m.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Mash mash = new Mash(
                70,
                steps.Object,
                "Empty");

            Assert.IsFalse(mash.IsValid());
        }
    }
}
