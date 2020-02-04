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
        public void Mash_InvalidMashStep_Invalid()
        {
            Mash_Steps steps = new Mash_Steps();

            Mock<IBeerXMLEntity> mockMashStep = new Mock<IBeerXMLEntity>();

       //     mockMashStep.Setup(m => m.IsValid()).Returns(false);

            steps.Add(mockMashStep.Object);

            Mash mash = new Mash(
                70,
                steps,
                "Empty");

            Assert.IsFalse(mash.IsValid());
        }
    }
}
