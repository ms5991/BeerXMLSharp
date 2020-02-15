using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BeerXMLSharp.UnitTests.OM.RecordSets
{
    [TestClass]
    public class RecipesTests
    {
        private Mock<Recipe> GetMockRecipe()
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

            style.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<Hops> hops = new Mock<Hops>();

            hops.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<Fermentables> ferms = new Mock<Fermentables>();

            ferms.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<Waters> waters = new Mock<Waters>();

            waters.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<Miscs> miscs = new Mock<Miscs>();

            miscs.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<Yeasts> yeasts = new Mock<Yeasts>();

            yeasts.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);
            
            Mock<Mash_Steps> steps = new Mock<Mash_Steps>();

            steps.Setup(m => m.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            Mock<Mash> mash = new Mock<Mash>(
                70,
                steps.Object,
                "Empty",
                1);
            mash.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            return new Mock<Recipe>(
                RecipeType.All_Grain,
                style.Object,
                "Michael",
                5,
                6.5,
                6.5,
                hops.Object,
                ferms.Object,
                miscs.Object,
                yeasts.Object,
                waters.Object,
                mash.Object,
                "Test",
                1);
        }

        [TestMethod]
        public void Recipes_Valid_Empty()
        {
            Recipes recipes = new Recipes();

            Assert.IsTrue(recipes.IsValid());
        }

        [TestMethod]
        public void Recipes_Valid_NonEmpty()
        {
            Recipes recipes = new Recipes();

            Mock<Recipe> recipe = GetMockRecipe();

            recipe.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            recipes.Add(recipe.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            Assert.IsTrue(recipes.IsValid(ref errorCode, suppressTypeCheck: true));
        }

        [TestMethod]
        public void Recipe_Valid_ErrorCode()
        {
            Recipes recipes = new Recipes();

            Mock<Recipe> recipe = GetMockRecipe();

            recipe.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            recipes.Add(recipe.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // need to suppress the type check because moq uses a different type
            recipes.IsValid(ref errorCode, suppressTypeCheck: true);

            Assert.AreEqual(ValidationCode.SUCCESS, errorCode);
        }

        [TestMethod]
        public void Recipes_Invalid_BadType()
        {
            Recipes recipes = new Recipes();

            Mock<Recipe> recipe = GetMockRecipe();

            recipe.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            recipes.Add(recipe.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            Assert.IsFalse(recipes.IsValid(ref errorCode, suppressTypeCheck: false));
        }

        [TestMethod]
        public void Recipes_Invalid_BadType_ErrorCode()
        {
            Recipes recipes = new Recipes();

            Mock<Recipe> recipe = GetMockRecipe();

            recipe.Setup(s => s.IsValid(ref It.Ref<ValidationCode>.IsAny)).Returns(true);

            recipes.Add(recipe.Object);

            ValidationCode errorCode = ValidationCode.SUCCESS;

            // do not suppress type check. Since moq uses a different type anyway,
            // there is no need to test with a different IRecord type
            recipes.IsValid(ref errorCode, suppressTypeCheck: false);

            Assert.AreEqual(ValidationCode.RECORD_SET_CONTAINS_INVALID_TYPE, errorCode);
        }
    }
}
