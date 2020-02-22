using BeerXMLSharp.OM;
using BeerXMLSharp.OM.RecordSets;
using BeerXMLSharp.Serialization;
using BeerXMLSharp.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeerXMLSharp.UnitTests.OM
{
    [TestClass]
    public class BeerXMLEntityBaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(BeerXMLInvalidObjectException))]
        public void GetBeerXML_Invalid_Exception()
        {
            Mock<BeerXMLEntityBase> entity = new Mock<BeerXMLEntityBase>();

            entity.Setup(e => e.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            entity.Object.GetBeerXML();
        }

        [TestMethod]

        public void Serializer_SerializeCalled()
        {
            Mock<BeerXMLEntityBase> entity = new Mock<BeerXMLEntityBase>();

            entity.Setup(e => e.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<IBeerXMLSerializer> serializer = new Mock<IBeerXMLSerializer>();

            string result = "result";
            serializer.Setup(s => s.Serialize(It.IsAny<IBeerXMLEntity>())).Returns(result);

            entity.Object.Serializer = serializer.Object;

            string serializedResult = entity.Object.GetBeerXML();

            serializer.Verify(s => s.Serialize(entity.Object));
        }
    }
}
