using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public sealed class Yeast : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public YeastType Type { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public YeastForm Form { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Amount { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public bool? Amount_Is_Weight { get; set; }

        [BeerXMLInclude()]
        public string Laboratory { get; set; }

        [BeerXMLInclude()]
        public string Product_Id { get; set; }

        [BeerXMLInclude()]
        public double? Min_Temperature { get; set; }

        [BeerXMLInclude()]
        public double? Max_Temperature { get; set; }

        [BeerXMLInclude()]
        public YeastFlocculation? Flocculation { get; set; }

        [BeerXMLInclude()]
        public double? Attenuation { get; set; }

        [BeerXMLInclude()]
        public string Best_For { get; set; }

        [BeerXMLInclude()]
        public int? Times_Cultured { get; set; }

        [BeerXMLInclude()]
        public int? Max_Reuse { get; set; }

        [BeerXMLInclude()]
        public bool? Add_To_Secondary { get; set; }

        #endregion

        internal Yeast()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Yeast"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="form">The form.</param>
        /// <param name="amountInKgOrLiters">The amount in kg or liters.</param>
        /// <param name="name">The name.</param>
        /// <param name="amountIsWeight">if set to <c>true</c> [amount is weight].</param>
        /// <param name="version">The version.</param>
        public Yeast(
            YeastType type,
            YeastForm form,
            double amountInKgOrLiters,
            string name,
            bool amountIsWeight = false,
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(amountInKgOrLiters);

            this.Type = type;
            this.Form = form;
            this.Amount = amountInKgOrLiters;
            this.Amount_Is_Weight = amountIsWeight;
        }
    }
}
