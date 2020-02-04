using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM
{
    public enum HopUse
    {
        Boil,
        Dry_Hop,
        Mash,
        First_Wort,
        Aroma
    }

    public enum HopType
    {
        Bittering,
        Aroma,
        Both
    }

    public enum HopForm
    {
        Pellet,
        Plug,
        Leaf
    }

    public enum FermentableType
    {
        Grain,
        Sugar,
        Extract,
        Dry_Extract,
        Adjunct
    }

    public enum YeastType
    {
        Ale,
        Lager,
        Wheat,
        Wine,
        Champagne
    }

    public enum YeastForm
    {
        Liquid,
        Dry,
        Slant,
        Culture
    }

    public enum YeastFlocculation
    {
        Low,
        Medium,
        High,
        Very_High
    }

    public enum MiscType
    {
        Spice,
        Fining,
        Water_Agent,
        Herb,
        Flavor,
        Other
    }

    public enum MiscUse
    {
        Boil,
        Mash,
        Primary,
        Secondary,
        Bottling
    }

    public enum StyleType
    {
        Lager,
        Ale,
        Mead,
        Wheat,
        Mixed,
        Cider
    }

    public enum MashStepType
    {
        Infusion,
        Temperature,
        Decoction
    }

    public enum RecipeType
    {
        Extract,
        Partial_Mash,
        All_Grain
    }

    [Flags]
    public enum ValidationCode
    {
        SUCCESS = 0,
        BATCH_OR_BOIL_SIZE_MISMATCH = 1,
        MISSING_MASH_STEP_FOR_NON_EXTRACT = 2,
        INVALID_DATE = 4,
        DECOTION_MISSING_INFUSE_AMOUNT = 8,
        GRAIN_DETAILS_ONLY_GRAIN_TYPE = 16,
        HOPPED_FERMENTABLE_EXTRACT_ONLY = 32,
        PRIMING_SUGAR_FOR_FORCED_CARBONATION = 64,
        EFFICIENCY_REQUIRED_FOR_GRAINS = 128,
        BOIL_VOLUME_REQUIRED_PARAMS_MISSING = 256,
        RECORD_SET_CONTAINS_INVALID_TYPE = 512
    }


    public static class EnumUtilities
    {
        public static string ConvertEnumToString(Enum enumValue)
        {
            return enumValue.ToString().Replace('_',' ');
        }
    }

}
