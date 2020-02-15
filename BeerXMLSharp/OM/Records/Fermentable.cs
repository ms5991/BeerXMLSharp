using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public class Fermentable : BeerXMLRecordBase
    {
        #region Required
        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public FermentableType Type { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Amount { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Yield { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Color { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public bool? Add_After_Boil { get; set; }

        [BeerXMLInclude()]
        public string Origin { get; set; }

        [BeerXMLInclude()]
        public string Supplier { get; set; }

        [BeerXMLInclude()]
        public double? Coarse_Fine_Diff { get; set; }

        [BeerXMLInclude()]
        public double? Moisture { get; set; }

        [BeerXMLInclude()]
        public double? Diastatic_Power { get; set; }

        [BeerXMLInclude()]
        public double? Protein { get; set; }

        [BeerXMLInclude()]
        public double? Max_In_Batch { get; set; }

        [BeerXMLInclude()]
        public bool? Recommended_Mash { get; set; }

        [BeerXMLInclude()]
        public double? Ibu_Gal_Per_Lb { get; set; }

        #endregion

        internal Fermentable()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fermentable"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="amountInKg">The amount in kg.</param>
        /// <param name="yieldPercent">The yield percent.</param>
        /// <param name="colorInLovibond">The color in lovibond.</param>
        /// <param name="version">The version.</param>
        public Fermentable(
            string name, 
            FermentableType type,
            double amountInKg,
            double yieldPercent,
            double colorInLovibond,
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(amountInKg);
            Validation.ValidatePercentileRange(yieldPercent);
            Validation.ValidateGreaterThanZero(colorInLovibond);

            this.Type = type;
            this.Amount = amountInKg;
            this.Yield = yieldPercent;
            this.Color = colorInLovibond;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        internal override bool IsValidInternal(ref ValidationCode errorCode)
        {
            bool result = true;

            if (this.Type != FermentableType.Adjunct &&
                this.Type != FermentableType.Grain)
            {
                // these properties are only appropriate for Adjunct and Grain types
                if (this.Coarse_Fine_Diff != null ||
                    this.Moisture != null ||
                    this.Diastatic_Power != null ||
                    this.Protein != null || 
                    (this.Recommended_Mash.HasValue && this.Recommended_Mash.Value))
                {
                    result = false;
                    errorCode |= ValidationCode.GRAIN_DETAILS_ONLY_GRAIN_TYPE;
                }
            }

            // Ibu Gal Per Lb is only suitable with Extract type
            if (this.Type != FermentableType.Extract &&
                this.Ibu_Gal_Per_Lb != null)
            {
                result = false;
                errorCode |= ValidationCode.HOPPED_FERMENTABLE_EXTRACT_ONLY;
            }

            return result;
        }
    }
}
