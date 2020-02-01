using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public sealed class Style : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public string Category { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public string Category_Number { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public string Style_Letter { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public string Style_Guide { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public StyleType Type { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double OG_Max { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double OG_Min { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public int IBU_Min { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public int IBU_Max { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public int Color_Min { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public int Color_Max { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public double? Carb_Min { get; set; }

        [BeerXMLInclude()]
        public double? Carb_Max { get; set; }

        [BeerXMLInclude()]
        public double? Abv_Min { get; set; }

        [BeerXMLInclude()]
        public double? Abv_Max { get; set; }

        [BeerXMLInclude()]
        public string Profile { get; set; }

        [BeerXMLInclude()]
        public string Ingredients { get; set; }

        [BeerXMLInclude()]
        public string Example { get; set; }

        #endregion

        internal Style()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="categoryNumber">The category number.</param>
        /// <param name="styleLetter">The style letter.</param>
        /// <param name="styleGuide">The style guide.</param>
        /// <param name="type">The type.</param>
        /// <param name="ogMax">The og maximum.</param>
        /// <param name="ogMin">The og minimum.</param>
        /// <param name="ibuMin">The ibu minimum.</param>
        /// <param name="ibuMax">The ibu maximum.</param>
        /// <param name="colorMin">The color minimum.</param>
        /// <param name="colorMax">The color maximum.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        public Style(
            string category,
            string categoryNumber,
            string styleLetter,
            string styleGuide,
            StyleType type,
            double ogMax,
            double ogMin,
            int ibuMin,
            int ibuMax,
            int colorMin,
            int colorMax,
            string name, 
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(ogMax);
            Validation.ValidateGreaterThanZero(ogMin);
            Validation.ValidateGreaterThanZero(ibuMin);
            Validation.ValidateGreaterThanZero(ibuMax);
            Validation.ValidateGreaterThanZero(colorMin);
            Validation.ValidateGreaterThanZero(colorMax);

            this.Category = category;
            this.Category_Number = categoryNumber;
            this.Style_Letter = styleLetter;
            this.Style_Guide = styleGuide;
            this.Type = type;
            this.OG_Min = ogMin;
            this.OG_Max = ogMax;
            this.IBU_Min = ibuMin;
            this.IBU_Max = ibuMax;
            this.Color_Min = colorMin;
            this.Color_Max = colorMax;
        }
    }
}
