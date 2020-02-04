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

            ValidationCode errorCode = ValidationCode.SUCCESS;
            Assert.IsFalse(ferm.IsValid(ref errorCode));
            Assert.AreEqual(ValidationCode.HOPPED_FERMENTABLE_EXTRACT_ONLY, errorCode);
        }

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam_Moisture()
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

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam_CoarseFineDiff()
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

            ferm.Coarse_Fine_Diff = 5;

            Assert.IsFalse(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam_DiastaticPower()
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

            ferm.Diastatic_Power = 5;

            Assert.IsFalse(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam_Protein()
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

            ferm.Protein = 5;

            Assert.IsFalse(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam_RecommendedMash()
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

            ferm.Recommended_Mash = true;

            Assert.IsFalse(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Valid_Extract_RecommendedMash_False()
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

            ferm.Recommended_Mash = false;

            Assert.IsTrue(ferm.IsValid());
        }

        [TestMethod]
        public void Fermentable_Invalid_Extract_InvalidParam_CorrectErrorCode()
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

            ferm.Protein = 5;

            ValidationCode errorCode = ValidationCode.SUCCESS;
            ferm.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.GRAIN_DETAILS_ONLY_GRAIN_TYPE, errorCode);
        }

        [TestMethod]
        public void Fermentable_Invalid_IBU_InvalidParam_CorrectErrorCode()
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

            ValidationCode errorCode = ValidationCode.SUCCESS;
            ferm.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.HOPPED_FERMENTABLE_EXTRACT_ONLY, errorCode);
        }

        [TestMethod]
        public void Fermentable_Invalid_CombinationErrorCode()
        {
            string fermentableName = "Ferm";
            FermentableType type = FermentableType.Dry_Extract;
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
            ferm.Moisture = 5;

            ValidationCode errorCode = ValidationCode.SUCCESS;

            ValidationCode expected = ValidationCode.HOPPED_FERMENTABLE_EXTRACT_ONLY | ValidationCode.GRAIN_DETAILS_ONLY_GRAIN_TYPE;

            ferm.IsValid(ref errorCode);

            Assert.AreEqual(expected, errorCode);
        }
    }
}
