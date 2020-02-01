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
        public void Equipment_Calc_Volume_True_Invalid()
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void Equipment_Calc_Volume_True_CannotSetBoilSize()
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

            equipment.Boil_Size = 5.5; 
        }
    }
}
