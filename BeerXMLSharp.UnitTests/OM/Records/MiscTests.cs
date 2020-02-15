using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class MiscTests
    {
        [TestMethod]
        public void Misc_Valid()
        {
            Misc misc = new Misc(
                MiscType.Fining,
                MiscUse.Boil,
                1.0,
                1.0,
                "Test");

            Assert.IsTrue(misc.IsValid());
        }

        [TestMethod]
        public void Misc_Valid_ErrorCode()
        {
            Misc misc = new Misc(
                MiscType.Fining,
                MiscUse.Boil,
                1.0,
                1.0,
                "Test");

            ValidationCode errorCode = ValidationCode.SUCCESS;
            misc.IsValid();

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }
    }
}
