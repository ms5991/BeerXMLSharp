using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class EquipmentTests
    {
        [TestMethod]
        public void Equipment_Calc_Volume_False_Valid()
        {
            double volume = 6.5;
            double boilSize = 6.5;
            double batchSize = 5;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                boilSize,
                batchSize,
                name);

            Assert.IsTrue(equipment.IsValid());
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Valid()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            Assert.IsTrue(equipment.IsValid());
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Invalid_TopUpWater()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            equipment.Top_Up_Water = null;

            Assert.IsFalse(equipment.IsValid());
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Invalid_TrubChillerLoss()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            equipment.Trub_Chiller_Loss = null;

            Assert.IsFalse(equipment.IsValid());
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Invalid_BoilTime()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            equipment.Boil_Time = null;

            Assert.IsFalse(equipment.IsValid());
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Invalid_EvapRate()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            equipment.Evap_Rate = null;

            Assert.IsFalse(equipment.IsValid());
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Invalid_CorrectErrorCode()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            equipment.Evap_Rate = null;

            ValidationCode errorCode = ValidationCode.SUCCESS;

            equipment.IsValid(ref errorCode);

            // only validation code should be missing params
            Assert.AreEqual(ValidationCode.BOIL_VOLUME_REQUIRED_PARAMS_MISSING, errorCode);
        }

        [TestMethod]
        public void Equipment_Calc_Volume_True_Correct_Volume()
        {
            double volume = 6.5;
            double batchSize = 5;
            double topUpWater = 3;
            double trubChillerLoss = 1;
            double boilTime = 60;
            double evapRate = 0.66;
            string name = "EquipmentName";

            Equipment equipment = new Equipment(
                volume,
                batchSize,
                name,
                topUpWater,
                trubChillerLoss,
                boilTime,
                evapRate);

            double expectedVolume = (batchSize - topUpWater - trubChillerLoss) * (1 + boilTime * evapRate);

            Assert.IsTrue(IsEqualWithEpsilon(expectedVolume, equipment.Boil_Size));
        }

        /// <summary>
        /// Helper to determine if two doubles are equal to a given epsilon
        /// </summary>
        private bool IsEqualWithEpsilon(double firstValue, double secondValue, double epsilon = 0.0001)
        {
            return Math.Abs(firstValue - secondValue) <= epsilon;
        }
    }
}
