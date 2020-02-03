using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public sealed class Misc : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public MiscType Type { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public MiscUse Use { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Time { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Amount { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public bool? Amount_Is_Weight { get; set; }

        [BeerXMLInclude()]
        public string Use_For { get; set; }

        #endregion

        internal Misc()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Misc"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="use">The use.</param>
        /// <param name="timeInMinutes">The time in minutes.</param>
        /// <param name="amountInKgOrLiters">The amount in kg or liters.</param>
        /// <param name="name">The name.</param>
        /// <param name="amountIsWeight">if set to <c>true</c> [amount is weight].</param>
        /// <param name="version">The version.</param>
        public Misc(
            MiscType type,
            MiscUse use,
            double timeInMinutes,
            double amountInKgOrLiters,
            string name, 
            bool amountIsWeight = false,
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(timeInMinutes);
            Validation.ValidateGreaterThanZero(amountInKgOrLiters);

            this.Type = type;
            this.Use = use;
            this.Time = timeInMinutes;
            this.Amount = amountInKgOrLiters;
            this.Amount_Is_Weight = amountIsWeight;
        }
    }
}
