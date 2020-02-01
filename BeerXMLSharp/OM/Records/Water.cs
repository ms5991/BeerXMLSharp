using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public sealed class Water : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Amount { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Calcium { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Bicarbonate { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Sulfate { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Chloride { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Sodium { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Magnesium { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public double? PH { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Water"/> class.
        /// </summary>
        /// <param name="amountInLiters">The amount in liters.</param>
        /// <param name="calciumPPM">The calcium PPM.</param>
        /// <param name="bicarbonatePPM">The bicarbonate PPM.</param>
        /// <param name="sulfatePPM">The sulfate PPM.</param>
        /// <param name="chloridePPM">The chloride PPM.</param>
        /// <param name="sodiumPPM">The sodium PPM.</param>
        /// <param name="magnesiumPPM">The magnesium PPM.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        public Water(
            double amountInLiters,
            double calciumPPM,
            double bicarbonatePPM,
            double sulfatePPM,
            double chloridePPM,
            double sodiumPPM,
            double magnesiumPPM,
            string name, 
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(amountInLiters);
            Validation.ValidateGreaterThanZero(calciumPPM);
            Validation.ValidateGreaterThanZero(bicarbonatePPM);
            Validation.ValidateGreaterThanZero(sulfatePPM);
            Validation.ValidateGreaterThanZero(chloridePPM);
            Validation.ValidateGreaterThanZero(sodiumPPM);
            Validation.ValidateGreaterThanZero(magnesiumPPM);

            this.Amount = amountInLiters;
            this.Calcium = calciumPPM;
            this.Bicarbonate = bicarbonatePPM;
            this.Sulfate = sulfatePPM;
            this.Chloride = chloridePPM;
            this.Sodium = sodiumPPM;
            this.Magnesium = magnesiumPPM;
        }
    }
}
