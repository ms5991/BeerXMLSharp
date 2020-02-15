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

        [TestMethod]
        public void Recipe_Valid_ErrorCode()
        {
            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test");

            ValidationCode errorCode = ValidationCode.SUCCESS;
            Assert.IsTrue(recipe.IsValid(ref errorCode));

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Recipe_InvalidStyle()
        {
            Mock<Style> style = new Mock<Style>(
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

            style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                style);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidHops()
        {
            Mock<Hops> hops = new Mock<Hops>();

            hops.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                null,
                hops);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidFerms()
        {
            Mock<Fermentables> ferms = new Mock<Fermentables>();

            ferms.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                null,
                null,
                ferms);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidWaters()
        {
            Mock<Waters> waters = new Mock<Waters>();

            waters.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                null,
                null,
                null,
                waters);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidMiscs()
        {
            Mock<Miscs> miscs = new Mock<Miscs>();

            miscs.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                null,
                null,
                null,
                null,
                miscs);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidYeasts()
        {
            Mock<Yeasts> yeasts = new Mock<Yeasts>();

            yeasts.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                null,
                null,
                null,
                null,
                null,
                yeasts);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidMash()
        {
            Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

            Mock<Mash> mash = new Mock<Mash>(
                70,
                steps.Object,
                "Empty",
                1);
            mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test",
                null,
                null,
                null,
                null,
                null,
                null,
                mash);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidEquipment()
        {
            Mock<Equipment> equip = new Mock<Equipment>(
                6.5,
                5.0,
                60,
                "Test",
                1);

            equip.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(false);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                5.0,
                6.5,
                60,
                "Test");

            recipe.Equipment = equip.Object;

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidEquipment_BoilSizeMismatch()
        {
            double equipBoilSize = 6.5;
            double recipeBoilSize = 8.5;

            double batchSize = 5.0;
            double volume = 6.5;
            string name = "Test";

            Equipment equip = new Equipment(
                volume,
                equipBoilSize,
                batchSize,
                name);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                batchSize,
                recipeBoilSize,
                60,
                "Test");

            recipe.Equipment = equip;

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidEquipment_BoilSizeMismatch_ErrorCode()
        {
            double equipBoilSize = 6.5;
            double recipeBoilSize = 8.5;

            double batchSize = 5.0;
            double volume = 6.5;
            string name = "Test";

            Equipment equip = new Equipment(
                volume,
                equipBoilSize,
                batchSize,
                name);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                batchSize,
                recipeBoilSize,
                60,
                "Test");

            recipe.Equipment = equip;

            ValidationCode errorCode = ValidationCode.SUCCESS;

            recipe.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.BATCH_OR_BOIL_SIZE_MISMATCH, errorCode);
        }

        [TestMethod]
        public void Recipe_InvalidEquipment_BatchSizeMismatch()
        {
            double equipBatchSize = 6.5;
            double recipeBatchSize = 8.5;

            double boilSize = 5.0;
            double volume = 6.5;
            string name = "Test";

            Equipment equip = new Equipment(
                volume,
                boilSize,
                equipBatchSize,
                name);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                recipeBatchSize,
                boilSize,
                60,
                "Test");

            recipe.Equipment = equip;

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_InvalidEquipment_BatchSizeMismatch_ErrorCode()
        {
            double equipBatchSize = 6.5;
            double recipeBatchSize = 8.5;

            double boilSize = 5.0;
            double volume = 6.5;
            string name = "Test";

            Equipment equip = new Equipment(
                volume,
                boilSize,
                equipBatchSize,
                name);

            Recipe recipe = GetRecipe(
                RecipeType.Extract,
                "Michael",
                recipeBatchSize,
                boilSize,
                60,
                "Test");

            recipe.Equipment = equip;

            ValidationCode errorCode = ValidationCode.SUCCESS;

            recipe.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.BATCH_OR_BOIL_SIZE_MISMATCH, errorCode);
        }

        [TestMethod]
        public void Recipe_Invalid_NonExtract_Missing_Steps()
        {
            Recipe recipe = GetRecipe(
                RecipeType.All_Grain,
                "Michael",
                5.0,
                6.5,
                60,
                "Test");

            Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

            Mock<Mash> mash = new Mock<Mash>(
                70,
                steps.Object,
                "Empty",
                1);

            Assert.IsFalse(recipe.IsValid());
        }

        [TestMethod]
        public void Recipe_Invalid_NonExtract_Missing_Steps_ErrorCode()
        {
            Recipe recipe = GetRecipe(
                RecipeType.All_Grain,
                "Michael",
                5.0,
                6.5,
                60,
                "Test");

            Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

            Mock<Mash> mash = new Mock<Mash>(
                70,
                steps.Object,
                "Empty",
                1);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            recipe.IsValid(ref errorCode);

            Assert.AreEqual(ValidationCode.MISSING_MASH_STEP_FOR_NON_EXTRACT, errorCode);
        }
    }
}
