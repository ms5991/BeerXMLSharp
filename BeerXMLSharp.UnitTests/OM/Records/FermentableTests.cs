using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class FermentableTests
    {
        [TestMethod]
        public void Fermentable_Valid()
        {
            string fermentableName = "Ferm";
            FermentableType type = FermentableType.Extract;
            double amount = 14;
            double yield = 50;
            double color = 40;

            Fermentable ferm = new Fermentable(
                fermentableName,
                type,
                amount,
                yield,
                color);

            Assert.IsTrue(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Invalid_NonExtract_IBU()
        {
            string fermentableName = "Ferm";
            FermentableType type = FermentableType.Grain;
            double amount = 14;
            double yield = 50;
            double color = 40;

            Fermentable ferm = new Fermentable(
                fermentableName,
                type,
                amount,
                yield,
                color);

            ferm.Ibu_Gal_Per_Lb = 5;

            Assert.IsFalse(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam()
        {
            string fermentableName = "Ferm";
            FermentableType type = FermentableType.Extract;
            double amount = 14;
            double yield = 50;
            double color = 40;

            Fermentable ferm = new Fermentable(
                fermentableName,
                type,
                amount,
                yield,
                color);

            ferm.Moisture = 5;

            Assert.IsFalse(ferm.IsValid());
        }
    }
}
