﻿using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public sealed class MashStep : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public MashStepType Type { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Step_Temp { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Step_Time { get; set; }

        #endregion

        #region Conditional

        [BeerXMLInclude(requirement: PropertyRequirement.CONDITIONAL)]
        public double? Infuse_Amount { get; set; }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public double? Ramp_Time { get; set; }

        [BeerXMLInclude()]
        public double? End_Temp { get; set; }

        #endregion

        /// <summary>
        /// Tag used in BeerXML. Override for this type to include underscore
        /// </summary>
        public override string TagName
        {
            get
            {
                return "Mash_Step".ToUpperInvariant();
            }
        }

        internal MashStep()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MashStep"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stepTemp">The step temporary.</param>
        /// <param name="stepTime">The step time.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        public MashStep(
            MashStepType type,
            double stepTemp,
            double stepTime,
            string name, 
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(stepTime);
            Validation.ValidateGreaterThanZero(stepTemp);

            this.Type = type;
            this.Step_Temp = stepTemp;
            this.Step_Time = stepTime;
        }


        /// <summary>
        /// Returns a bool indicating if this instance's conditional properties will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        internal override bool ValidateConditionalProperties(ref ValidationCode errorCode)
        {
            bool result = true;
            if (this.Type == MashStepType.Decoction &&
                this.Infuse_Amount != null)
            {
                result = false;
                errorCode |= ValidationCode.DECOTION_MISSING_INFUSE_AMOUNT;
            }

            return result;
        }
    }
}
