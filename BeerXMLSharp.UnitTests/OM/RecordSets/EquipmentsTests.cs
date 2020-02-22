using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class EquipmentsTests
    {
        private Mock<Equipment> GetMockEquipment()
        {
            return new Mock<Equipment>(
                5.5,
                5.0,
                6.5,
                "Test",
                1);
        }

        [TestMethod]
        public void Equipments_Valid_Empty()
        {
            Equipments equipments = new Equipments();

            Assert.IsTrue(equipments.IsValid());
        }

        [TestMethod]
        public void Equipments_Valid_NonEmpty()
        {
            Equipments equipments = new Equipments();

            Mock<Equipment> equipment = GetMockEquipment();

            equipment.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            equipments.Add(equipment.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(equipments.IsValidRecordSet(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Equipment_Valid_ErrorCode()
        {
            Equipments equipments = new Equipments();

            Mock<Equipment> equipment = GetMockEquipment();

            equipment.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            equipments.Add(equipment.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            equipments.IsValidRecordSet(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Equipments_Invalid_BadType()
        {
            Equipments equipments = new Equipments();

            Mock<Equipment> equipment = GetMockEquipment();

            equipment.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            equipments.Add(equipment.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(equipments.IsValidRecordSet(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Equipments_Invalid_BadType_ErrorCode()
        {
            Equipments equipments = new Equipments();

            Mock<Equipment> equipment = GetMockEquipment();

            equipment.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            equipments.Add(equipment.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            equipments.IsValidRecordSet(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
