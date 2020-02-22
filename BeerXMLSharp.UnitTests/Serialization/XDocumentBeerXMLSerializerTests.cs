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

namespace BeerXMLSharp.UnitTests.Serialization
{
    [TestClass]
    public class XDocumentBeerXMLSerializerTests
    {
        [TestMethod]
        public void DeserializeCorrectType_FullRecipe()
        {
            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = CommonUtilities.GetTestXmlStream(CommonUtilities.TEST_XML_DRY_STOUT))
            {
                streamFactory.Setup(f => f.GetFileStream(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(ms);

                XDocumentBeerXMLSerializer s = new XDocumentBeerXMLSerializer();

                s.StreamFactory = streamFactory.Object;

                IBeerXMLEntity e = s.Deserialize(It.IsAny<string>());

                Assert.AreEqual(typeof(Recipes), e.GetType());
            }
        }

        [TestMethod]
        public void DeserializeHop_CorrectUse_WithUnderscore()
        {
            HopUse use = HopUse.Dry_Hop;

            string xml = $@"
                <HOP>
                    <NAME>Goldings, East Kent</NAME>
                    <VERSION>1</VERSION>
                    <ALPHA>5.0</ALPHA>
                    <AMOUNT>0.0638</AMOUNT>
                    <USE>{EnumUtilities.ConvertEnumToString(use)}</USE>
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

                Assert.AreEqual(use, hop.Use);
            }
        }

        [TestMethod]
        public void DeserializeHop_InvalidResultIsNotValid()
        {
            MashStepType type = MashStepType.Decoction;

            string xml = $@"
                <MASH_STEP>
                    <NAME>Conversion Step, 68C</NAME>
                    <VERSION>1</VERSION>
                    <TYPE>{EnumUtilities.ConvertEnumToString(type)}</TYPE>
                    <STEP_TEMP>68.0</STEP_TEMP>
                    <STEP_TIME>60.0</STEP_TIME>
                    <INFUSE_AMOUNT>10.0</INFUSE_AMOUNT>
                </MASH_STEP>";

            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = CommonUtilities.GetTestXmlStream(xml))
            {
                streamFactory.Setup(f => f.GetFileStream(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(ms);

                XDocumentBeerXMLSerializer s = new XDocumentBeerXMLSerializer();

                s.StreamFactory = streamFactory.Object;

                Mash_Step step = (Mash_Step)s.Deserialize(It.IsAny<string>());

                Assert.IsFalse(step.IsValid());
            }
        }
    }
}
