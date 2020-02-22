﻿using BeerXMLSharp.OM;
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
        #region TestXML

        private static readonly string TEST_XML_DRY_STOUT = @"<?xml version=""1.0"" encoding=""iso-8859-1""?>
<!-- http://www.beerxml.com/beerxml.htm -->
<RECIPES>
  <RECIPE>
    <NAME>Dry Stout</NAME>
    <VERSION>1</VERSION>
    <TYPE>All Grain</TYPE>
    <BREWER>Brad Smith</BREWER>
    <BATCH_SIZE>18.93</BATCH_SIZE>
    <BOIL_SIZE>20.82</BOIL_SIZE>
    <BOIL_TIME>60.0</BOIL_TIME>
    <EFFICIENCY>72.0</EFFICIENCY>
    <TASTE_NOTES>Nice dry Irish stout with a warm body but low starting gravity much like the famous drafts.</TASTE_NOTES>
    <TASTE_RATING>41</TASTE_RATING>
    <DATE>3 Jan 04</DATE>
    <OG>1.036</OG>
    <FG>1.012</FG>
    <CARBONATION>2.1</CARBONATION>
    <CARBONATION_USED>Kegged</CARBONATION_USED>
    <AGE>24.0</AGE>
    <AGE_TEMP>17.0</AGE_TEMP>
    <FERMENTATION_STAGES>2</FERMENTATION_STAGES>
    <STYLE>
      <NAME>Dry Stout</NAME>
      <CATEGORY>Stout</CATEGORY>
      <CATEGORY_NUMBER>16</CATEGORY_NUMBER>
      <STYLE_LETTER>A</STYLE_LETTER>
      <STYLE_GUIDE>BJCP</STYLE_GUIDE>
      <VERSION>1</VERSION>
      <TYPE>Ale</TYPE>
      <OG_MIN>1.035</OG_MIN>
      <OG_MAX>1.050</OG_MAX>
      <FG_MIN>1.007</FG_MIN>
      <FG_MAX>1.011</FG_MAX>
      <IBU_MIN>30.0</IBU_MIN>
      <IBU_MAX>50.0</IBU_MAX>
      <COLOR_MIN>35.0</COLOR_MIN>
      <COLOR_MAX>200.0</COLOR_MAX>
      <ABV_MIN>3.2</ABV_MIN>
      <ABV_MAX>5.5</ABV_MAX>
      <CARB_MIN>1.6</CARB_MIN>
      <CARB_MAX>2.1</CARB_MAX>
      <NOTES>Famous Irish Stout.Dry, roasted, almost coffee like flavor.  Often soured with pasteurized sour beer.  Full body perception due to flaked barley, though starting gravity may be low.  Dry roasted flavor.</NOTES>
    </STYLE>
    <HOPS>
      <HOP>
        <NAME>Goldings, East Kent</NAME>
        <VERSION>1</VERSION>
        <ALPHA>5.0</ALPHA>
        <AMOUNT>0.0638</AMOUNT>
        <USE>Boil</USE>
        <TIME>60.0</TIME>
        <NOTES>Great all purpose UK hop for ales, stouts, porters</NOTES>
      </HOP>
    </HOPS>
    <FERMENTABLES>
      <FERMENTABLE>
        <NAME>Pale Malt (2 row) UK</NAME>
        <VERSION>1</VERSION>
        <AMOUNT>2.27</AMOUNT>
        <TYPE>Grain</TYPE>
        <YIELD>78.0</YIELD>
        <COLOR>3.0</COLOR>
        <ORIGIN>United Kingdom</ORIGIN>
        <SUPPLIER>Fussybrewer Malting</SUPPLIER>
        <NOTES>All purpose base malt for English styles</NOTES>
        <COARSE_FINE_DIFF>1.5</COARSE_FINE_DIFF>
        <MOISTURE>4.0</MOISTURE>
        <DIASTATIC_POWER>45.0</DIASTATIC_POWER>
        <PROTEIN>10.2</PROTEIN>
        <MAX_IN_BATCH>100.0</MAX_IN_BATCH>
      </FERMENTABLE>
      <FERMENTABLE>
        <NAME>Barley, Flaked</NAME>
        <VERSION>1</VERSION>
        <AMOUNT>0.91</AMOUNT>
        <TYPE>Grain</TYPE>
        <YIELD>70.0</YIELD>
        <COLOR>2.0</COLOR>
        <ORIGIN>United Kingdom</ORIGIN>
        <SUPPLIER>Fussybrewer Malting</SUPPLIER>
        <NOTES>Adds body to porters and stouts, must be mashed</NOTES>
        <COARSE_FINE_DIFF>1.5</COARSE_FINE_DIFF>
        <MOISTURE>9.0</MOISTURE>
        <DIASTATIC_POWER>0.0</DIASTATIC_POWER>
        <PROTEIN>13.2</PROTEIN>
        <MAX_IN_BATCH>20.0</MAX_IN_BATCH>
        <RECOMMEND_MASH>TRUE</RECOMMEND_MASH>
      </FERMENTABLE>
      <FERMENTABLE>
        <NAME>Black Barley</NAME>
        <VERSION>1</VERSION>
        <AMOUNT>0.45</AMOUNT>
        <TYPE>Grain</TYPE>
        <YIELD>78.0</YIELD>
        <COLOR>500.0</COLOR>
        <ORIGIN>United Kingdom</ORIGIN>
        <SUPPLIER>Fussybrewer Malting</SUPPLIER>
        <NOTES>Unmalted roasted barley for stouts, porters</NOTES>
        <COARSE_FINE_DIFF>1.5</COARSE_FINE_DIFF>
        <MOISTURE>5.0</MOISTURE>
        <DIASTATIC_POWER>0.0</DIASTATIC_POWER>
        <PROTEIN>13.2</PROTEIN>
        <MAX_IN_BATCH>10.0</MAX_IN_BATCH>
      </FERMENTABLE>
    </FERMENTABLES>
    <MISCS>
      <MISC>
        <NAME>Irish Moss</NAME>
        <VERSION>1</VERSION>
        <TYPE>Fining</TYPE>
        <USE>Boil</USE>
        <TIME>15.0</TIME>
        <AMOUNT>0.010</AMOUNT>
        <NOTES>Used as a clarifying agent during the last few minutes of the boil</NOTES>
      </MISC>
    </MISCS>
    <WATERS>
      <WATER>
        <NAME>Burton on Trent, UK</NAME>
        <VERSION>1</VERSION>
        <AMOUNT>20.0</AMOUNT>
        <CALCIUM>295.0</CALCIUM>
        <MAGNESIUM>45.0</MAGNESIUM>
        <SODIUM>55.0</SODIUM>
        <SULFATE>725.0</SULFATE>
        <CHLORIDE>25.0</CHLORIDE>
        <BICARBONATE>300.0</BICARBONATE>
        <PH>8.0</PH>
        <NOTES>
          Use for distinctive pale ales strongly hopped.Very hard water accentuates the hops flavor.Example: Bass Ale
        </NOTES>
      </WATER>
    </WATERS>
    <YEASTS>
      <YEAST>
        <NAME>Irish Ale</NAME>
        <TYPE>Ale</TYPE>
        <VERSION>1</VERSION>
        <FORM>Liquid</FORM>
        <AMOUNT>0.250</AMOUNT>
        <LABORATORY>Wyeast Labs</LABORATORY>
        <PRODUCT_ID>1084</PRODUCT_ID>
        <MIN_TEMPERATURE>16.7</MIN_TEMPERATURE>
        <MAX_TEMPERATURE>22.2</MAX_TEMPERATURE>
        <ATTENUATION>73.0</ATTENUATION>
        <NOTES>Dry, fruity flavor characteristic of stouts.Full bodied, dry, clean flavor. </NOTES>
        <BEST_FOR>Irish Dry Stouts</BEST_FOR>
        <FLOCCULATION>Medium</FLOCCULATION>
      </YEAST>
    </YEASTS>
    <MASH>
      <NAME>Single Step Infusion, 68 C</NAME>
      <VERSION>1</VERSION>
      <GRAIN_TEMP>22.0</GRAIN_TEMP>
      <MASH_STEPS>
        <MASH_STEP>
          <NAME>Conversion Step, 68C</NAME>
          <VERSION>1</VERSION>
          <TYPE>Infusion</TYPE>
          <STEP_TEMP>68.0</STEP_TEMP>
          <STEP_TIME>60.0</STEP_TIME>
          <INFUSE_AMOUNT>10.0</INFUSE_AMOUNT>
        </MASH_STEP>
      </MASH_STEPS>
    </MASH>
  </RECIPE>
</RECIPES>";
        #endregion

        /// <summary>
        /// Helper method that gets a memory stream populated
        /// with valid BeerXML for testing
        /// </summary>
        /// <returns></returns>
        private MemoryStream GetTestXmlStream(string xml)
        {
            MemoryStream ms = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(ms, Encoding.UTF8, xml.Length, leaveOpen: true))
            {
                writer.Write(xml);
            }

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }

        [TestMethod]
        public void DeserializeCorrectType_FullRecipe()
        {
            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = GetTestXmlStream(TEST_XML_DRY_STOUT))
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
                    <USE>{use.ToString()}</USE>
                    <TIME>60.0</TIME>
                    <NOTES>Great all purpose UK hop for ales, stouts, porters</NOTES>
                </HOP>";

            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = GetTestXmlStream(xml))
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
                    <TYPE>{type.ToString()}</TYPE>
                    <STEP_TEMP>68.0</STEP_TEMP>
                    <STEP_TIME>60.0</STEP_TIME>
                    <INFUSE_AMOUNT>10.0</INFUSE_AMOUNT>
                </MASH_STEP>";

            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = GetTestXmlStream(xml))
            {
                streamFactory.Setup(f => f.GetFileStream(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(ms);

                XDocumentBeerXMLSerializer s = new XDocumentBeerXMLSerializer();

                s.StreamFactory = streamFactory.Object;

                Mash_Step step = (Mash_Step)s.Deserialize(It.IsAny<string>());

                Assert.IsFalse(step.IsValid());
            }
        }

        [TestMethod]
        public void DeserializeHop_Invalid_NullRequiredParameter()
        {
            HopUse use = HopUse.Dry_Hop;

            string xml = $@"
                <HOP>
                    <VERSION>1</VERSION>
                    <ALPHA>5.0</ALPHA>
                    <AMOUNT>0.0638</AMOUNT>
                    <USE>{use.ToString()}</USE>
                    <TIME>60.0</TIME>
                    <NOTES>Great all purpose UK hop for ales, stouts, porters</NOTES>
                </HOP>";

            Mock<IStreamFactory> streamFactory = new Mock<IStreamFactory>();

            using (MemoryStream ms = GetTestXmlStream(xml))
            {
                streamFactory.Setup(f => f.GetFileStream(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(ms);

                XDocumentBeerXMLSerializer s = new XDocumentBeerXMLSerializer();

                s.StreamFactory = streamFactory.Object;

                Hop hop = (Hop)s.Deserialize(It.IsAny<string>());

                Assert.AreEqual(use, hop.Use);
            }
        }
    }
}
