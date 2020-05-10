using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
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

            using (MemoryStream ms = new MemoryStream())
            {
                ValidationCode errorCode = entity.Object.GetBeerXML(ms);
            }
        }

        [TestMethod]

        public void Serializer_SerializeCalled()
        {
            Mock<BeerXMLEntityBase> entity = new Mock<BeerXMLEntityBase>();

            entity.Setup(e => e.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<IBeerXMLSerializer> serializer = new Mock<IBeerXMLSerializer>();

            string result = "result";
           // serializer.Setup(s => s.Serialize(It.IsAny<IBeerXMLEntity>(), It.IsAny<Stream>())).Returns(result);

            entity.Object.Serializer = serializer.Object;

            using (MemoryStream ms = new MemoryStream())
            {
                ValidationCode errorCode = entity.Object.GetBeerXML(ms);
                serializer.Verify(s => s.Serialize(entity.Object, ms));
            }

        }

        [TestMethod]

        public void Deserialized_MissingRequiredParam_IsValid_False()
        {
            string xml = $@"
                <HOP>
                    <VERSION>1</VERSION>
                    <ALPHA>5.0</ALPHA>
                    <AMOUNT>0.0638</AMOUNT>
                    <USE>Dry Hop</USE>
                    <TIME>60.0</TIME>
                    <NOTES>Great all purpose UK hop for ales, stouts, porters</NOTES>
                </HOP>";

            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = CommonUtilities.GetTestXmlStream(xml))
            {
                streamFactory.Setup(f => f.GetFileStream(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(ms);

                XDocumentBeerXMLSerializer s = new XDocumentBeerXMLSerializer();

                s.StreamFactory = streamFactory.Object;

                Hop hop = (Hop)s.Deserialize(It.IsAny<string>());

                Assert.IsFalse(hop.IsValid());
            }
        }

        [TestMethod]

        public void Deserialized_MissingRequiredParam_IsValid_False_ErrorCode()
        {
            string xml = $@"
                <HOP>
                    <VERSION>1</VERSION>
                    <ALPHA>5.0</ALPHA>
                    <AMOUNT>0.0638</AMOUNT>
                    <USE>Dry Hop</USE>
                    <TIME>60.0</TIME>
                    <NOTES>Great all purpose UK hop for ales, stouts, porters</NOTES>
                </HOP>";

            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = CommonUtilities.GetTestXmlStream(xml))
            {
                streamFactory.Setup(f => f.GetFileStream(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(ms);

                XDocumentBeerXMLSerializer s = new XDocumentBeerXMLSerializer();

                s.StreamFactory = streamFactory.Object;

                Hop hop = (Hop)s.Deserialize(It.IsAny<string>());

                ValidationCode errorCode = ValidationCode.SUCCESS;
                hop.IsValid(ref errorCode);

                Assert.AreEqual(ValidationCode.MISSING_REQUIRED_PROPERTY, errorCode);
            }
        }
    }
}
