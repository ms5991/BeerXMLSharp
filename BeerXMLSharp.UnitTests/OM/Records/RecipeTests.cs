using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.Records
{
    [TestClass]
    public class RecipeTests
    {
        private Recipe GetRecipe(
            RecipeType type,
            string brewer,
            double batchSize,
            double boilSize,
            double boilTime,
            string name,
            Mock<Style> style = null,
            Mock<Hops> hops = null,
            Mock<Fermentables> ferms = null,
            Mock<Waters> waters = null,
            Mock<Miscs> miscs = null,
            Mock<Yeasts> yeasts = null,
            Mock<Mash> mash = null)
        {
            if (style == null)
            {
                style = new Mock<Style>(
                    "Category",
                    "1",
                    "A",
                    "Guide",
                    StyleType.Ale,
                    1.06,
                    1.05,
                    60.0,
                    70.0,
                    70.0,
                    70.0,
                    "Style",
                    1);

                style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            if (hops == null)
            {
                hops = new Mock<Hops>();

                hops.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            if (ferms == null)
            {
                ferms = new Mock<Fermentables>();

                ferms.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            if (waters == null)
            {
                waters = new Mock<Waters>();

                waters.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            if (miscs == null)
            {
                miscs = new Mock<Miscs>();

                miscs.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            if (yeasts == null)
            {
                yeasts = new Mock<Yeasts>();

                yeasts.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            if (mash == null)
            {
                Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

                steps.Setup(m => m.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

                mash = new Mock<Mash>(
                    70,
                    steps.Object,
                    "Empty",
                    1);
                mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            }

            return new Recipe(
                type,
                style.Object,
                brewer,
                batchSize,
                boilSize,
                boilTime,
                hops.Object,
                ferms.Object,
                miscs.Object,
                yeasts.Object,
                waters.Object,
                mash.Object,
                name);
        }

        [TestMethod]
        public void Recipe_Valid()
        {
            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test");
            Assert.IsTrue(recipe.IsValid());
        }
    }
}
