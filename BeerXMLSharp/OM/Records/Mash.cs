﻿using BeerXMLSharp.OM.RecordSets;
using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public class Mash : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Grain_Temp { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public Mash_Steps Mash_Steps { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public double? Tun_Temp { get; set; }

        [BeerXMLInclude()]
        public double? Sparge_Temp { get; set; }

        [BeerXMLInclude()]
        public double? PH { get; set; }

        [BeerXMLInclude()]
        public double? Tun_Weight { get; set; }

        [BeerXMLInclude()]
        public double? Tun_Specific_Heat { get; set; }

        [BeerXMLInclude()]
        public double? Equip_Adjust { get; set; }

        #endregion

        #region Extension properties

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Display_Grain_Temp { get; set; }

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Display_Tun_Temp { get; set; }

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Display_Sparge_Temp { get; set; }

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Display_Tun_Weight { get; set; }

        #endregion

        internal Mash()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mash"/> class.
        /// </summary>
        /// <param name="grainTemp">The grain temporary.</param>
        /// <param name="mashSteps">The mash steps.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        public Mash(
            double grainTemp,
            Mash_Steps mashSteps,
            string name, 
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(grainTemp);
            Validation.ValidateNotNull(mashSteps);

            this.Grain_Temp = grainTemp;
            this.Mash_Steps = mashSteps;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        protected override bool IsValidRecord(ref ValidationCode errorCode)
        {
            return this.Mash_Steps.IsValid(ref errorCode);
        }
    }
}
