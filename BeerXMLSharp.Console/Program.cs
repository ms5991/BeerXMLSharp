using BeerXMLSharp.OM;
using BeerXMLSharp.OM.Records;
using BeerXMLSharp.OM.RecordSets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeerXMLSharp.ConsoleDisplay
{
    public class Program
    {
        public static void TestSerialize()
        {
            Hops hops = new Hops();
            Hop citra = new Hop("Citra", 5, 1.2, HopUse.First_Wort, 60) { Notes = "I like this hops" };
            Hop centennial = new Hop("Centennial", 4.5, 1.2, HopUse.Boil, 60) { Type = HopType.Aroma };
            hops.Add(citra);
            hops.Add(centennial);

            Style style = new Style("Ale", "1", "A", "Guide", StyleType.Ale, 1.06, 1.058, 60, 80, 2, 5, "EJuice");

            Fermentables fermentables = new Fermentables();
            Fermentable f = new Fermentable("Pale extract", FermentableType.Dry_Extract, 14, 80, 40) { Origin = "Micro Homebrew" };

            fermentables.Add(f);

            Miscs miscs = new Miscs();

            Yeasts yeasts = new Yeasts();
            Yeast y = new Yeast(YeastType.Ale, YeastForm.Liquid, 3, "Wyeast", true) { Attenuation = 77, Max_Temperature = 70, Min_Temperature = 65 };
            yeasts.Add(y);

            Waters waters = new Waters();
            Water w = new Water(19, 1, 1, 1, 1, 2, 3, "Tap") { PH = 7 };
            waters.Add(w);

            Mash_Steps steps = new Mash_Steps();
            Mash_Step step1 = new Mash_Step(MashStepType.Infusion, 160, 30, "Soak");
            steps.Add(step1);

            Mash mash = new Mash(68, steps, "Step1");


            Recipe r = new Recipe(
                RecipeType.Extract,
                style,
                "Bob Jones",
                5,
                6.5,
                60,
                hops,
                fermentables,
                miscs,
                yeasts,
                waters,
                mash,
                "Elvis Juice");

            Recipes allR = new Recipes();

            allR.Add(r);

            using (MemoryStream ms = new MemoryStream())
            {
                ValidationCode errorCode = allR.GetBeerXML(ms);
                Console.WriteLine(Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length));
            }
        }

        public static void Deserialize()
        {
            string file = @"C:\Users\ms599\source\repos\BeerXMLSharp\BeerXMLSharp.Console\Examples\VanillaCreamAle.xml";

            IBeerXMLEntity entity = BeerXML.Deserialize(file);

            entity.AllowInvalidSerialization = true;

            using (MemoryStream ms = new MemoryStream())
            {
                ValidationCode errorCode = entity.GetBeerXML(ms);
                Console.WriteLine(Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length));
            }

        }

        public static object Get()
        {
            return null;
        }

        public static void Main(string[] args)
        {
            Deserialize();
        }
    }
}
