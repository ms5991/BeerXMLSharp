using BeerXMLSharp.OM.RecordSets;
using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public class Recipe : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public RecipeType Type { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Style Style { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public string Brewer { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Batch_Size { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Boil_Size { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Boil_Time { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Hops Hops { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Fermentables Fermentables { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Miscs Miscs { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Yeasts Yeasts { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Waters Waters { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Mash Mash { get; set; }

        #endregion

        #region Conditional

        [BeerXMLInclude(requirement: PropertyRequirement.CONDITIONAL)]
        public double? Efficiency { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public Equipment Equipment { get; set; }

        [BeerXMLInclude()]
        public string Asst_Brewer { get; set; }

        [BeerXMLInclude()]
        public string Taste_Notes { get; set; }

        private double? _tasteRating = null;
        [BeerXMLInclude()]
        public double? Taste_Rating
        {
            get
            {
                return this._tasteRating;
            }
            set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentException("Taste rating must be between 0 and 50");
                }

                this._tasteRating = value;
            }
        }


        [BeerXMLInclude()]
        public double? OG { get; set; }

        [BeerXMLInclude()]
        public double? FG { get; set; }

        [BeerXMLInclude()]
        public int? Fermentation_Stages { get; set; }

        [BeerXMLInclude()]
        public double? Primary_Age { get; set; }

        [BeerXMLInclude()]
        public double? Primary_Temp { get; set; }

        [BeerXMLInclude()]
        public double? Secondary_Age { get; set; }

        [BeerXMLInclude()]
        public double? Secondary_Temp { get; set; }

        [BeerXMLInclude()]
        public double? Tertiary_Age { get; set; }

        [BeerXMLInclude()]
        public double? Tertiary_Temp { get; set; }

        [BeerXMLInclude()]
        public double? Age { get; set; }

        [BeerXMLInclude()]
        public double? Age_Temp { get; set; }

        [BeerXMLInclude()]
        public string Date { get; set; }

        [BeerXMLInclude()]
        public double? Carbonation { get; set; }

        [BeerXMLInclude()]
        public bool? Forced_Carbonation { get; set; }

        [BeerXMLInclude()]
        public string Priming_Sugar_Name { get; set; }

        [BeerXMLInclude()]
        public double? Carbonation_Temp { get; set; }

        [BeerXMLInclude()]
        public double? Priming_Sugar_Equiv { get; set; }

        [BeerXMLInclude()]
        public double? Keg_Priming_Factor { get; set; }

        #endregion

        internal Recipe()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="style">The style.</param>
        /// <param name="brewer">The brewer.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <param name="boilSize">Size of the boil.</param>
        /// <param name="boilTime">The boil time.</param>
        /// <param name="hops">The hops.</param>
        /// <param name="fermentables">The fermentables.</param>
        /// <param name="miscs">The miscs.</param>
        /// <param name="yeasts">The yeasts.</param>
        /// <param name="waters">The waters.</param>
        /// <param name="mash">The mash.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        public Recipe(
            RecipeType type,
            Style style,
            string brewer,
            double batchSize,
            double boilSize,
            double boilTime,
            Hops hops,
            Fermentables fermentables,
            Miscs miscs,
            Yeasts yeasts,
            Waters waters,
            Mash mash,
            string name, 
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(batchSize);
            Validation.ValidateGreaterThanZero(boilSize);
            Validation.ValidateGreaterThanZero(boilTime);
            Validation.ValidateNotNull(style);
            Validation.ValidateNotNull(hops);
            Validation.ValidateNotNull(fermentables);
            Validation.ValidateNotNull(miscs);
            Validation.ValidateNotNull(yeasts);
            Validation.ValidateNotNull(waters);
            Validation.ValidateNotNull(mash);

            this.Type = type;
            this.Style = style;
            this.Brewer = brewer;
            this.Batch_Size = batchSize;
            this.Boil_Size = boilSize;
            this.Boil_Time = boilTime;
            this.Hops = hops;
            this.Fermentables = fermentables;
            this.Miscs = miscs;
            this.Yeasts = yeasts;
            this.Waters = waters;
            this.Mash = mash;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        protected override bool IsValidRecord(ref ValidationCode errorCode)
        {
            bool result = true;

            // Equipment and Recipe batch/boil sizes must be the same
            // if Equipment is present
            if (this.Equipment != null &&
                (!this.Batch_Size.IsEqualWithEpsilon(this.Equipment.Batch_Size) ||
                 !this.Boil_Size.IsEqualWithEpsilon(this.Equipment.Boil_Size)))
            {
                result = false;
                errorCode |= ValidationCode.BATCH_OR_BOIL_SIZE_MISMATCH;
            }

            // non-extract recipes need at least one mash step
            if (this.Type != RecipeType.Extract)
            {
                if (this.Mash.Mash_Steps.Count == 0)
                {
                    result = false;
                    errorCode |= ValidationCode.MISSING_MASH_STEP_FOR_NON_EXTRACT;
                }
            }

            DateTime parsedDate;
            if (this.Date != null && 
                !DateTime.TryParse(this.Date, out parsedDate))
            {
                result = false;
                errorCode |= ValidationCode.INVALID_DATE;
            }

            // priming sugar name is only valid when forced carbonation is false
            if (this.Forced_Carbonation .HasValue &&
                this.Forced_Carbonation.Value &&
                !string.IsNullOrEmpty(this.Priming_Sugar_Name))
            {
                result = false;
                errorCode |= ValidationCode.PRIMING_SUGAR_FOR_FORCED_CARBONATION;
            }

            result &= this.ValidateBeerProperties(ref errorCode);

            return result;
        }

        /// <summary>
        /// Returns a bool indicating if this instance's conditional properties will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        protected override bool ValidateConditionalProperties(ref ValidationCode errorCode)
        {
            bool result = true;
            // efficiency is required for partial masah and all grain
            if (this.Type == RecipeType.Partial_Mash || this.Type == RecipeType.All_Grain)
            {
                if (this.Efficiency == null)
                {
                    result = false;
                    errorCode |= ValidationCode.EFFICIENCY_REQUIRED_FOR_GRAINS;
                }
            }

            return result;
        }

        /// <summary>
        /// Validates the properties that are IBeerXMLEntities.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns></returns>
        private bool ValidateBeerProperties(ref ValidationCode errorCode)
        {
            bool result = true;

            result &= this.Hops.IsValid(ref errorCode);
            result &= this.Fermentables.IsValid(ref errorCode);
            result &= this.Mash.IsValid(ref errorCode);
            result &= this.Miscs.IsValid(ref errorCode);
            result &= this.Style.IsValid(ref errorCode);
            result &= this.Waters.IsValid(ref errorCode);
            result &= this.Yeasts.IsValid(ref errorCode);

            if (this.Equipment != null)
            {
                result &= this.Equipment.IsValid(ref errorCode);
            }

            return result;
        }
    }
}
