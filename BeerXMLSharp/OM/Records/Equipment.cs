using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.Records
{
    public sealed class Equipment : BeerXMLRecordBase
    {
        #region Required

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Volume { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Batch_Size { get; set; }

        private double _boilSize;

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Boil_Size
        {
            get
            {
                if (this.Calc_Boil_Volume.HasValue &&
                    this.Calc_Boil_Volume.Value)
                {
                    return this.CalculateBoilVolume();
                }

                return _boilSize;
            }
            set
            {
                if (this.Calc_Boil_Volume.HasValue &&
                    this.Calc_Boil_Volume.Value)
                {
                    throw new InvalidOperationException("Cannot set the value of Boil_Size when Calc_Boil_Volume is true");
                }

                _boilSize = value;
            }
        }

        #endregion

        #region Optional

        [BeerXMLInclude()]
        public double? Tun_Volume { get; set; }

        [BeerXMLInclude()]
        public double? Tun_Weight { get; set; }

        [BeerXMLInclude()]
        public double? Tun_Specific_Heat { get; set; }

        [BeerXMLInclude()]
        public double? Top_Up_Water { get; set; }

        [BeerXMLInclude()]
        public double? Trub_Chiller_Loss { get; set; }

        [BeerXMLInclude()]
        public double? Evap_Rate { get; set; }

        [BeerXMLInclude()]
        public double? Boil_Time { get; set; }

        [BeerXMLInclude()]
        public bool? Calc_Boil_Volume { get; internal set; }

        [BeerXMLInclude()]
        public double? Lauter_Deadspace { get; set; }

        [BeerXMLInclude()]
        public double? Top_Up_Kettle { get; set; }

        [BeerXMLInclude()]
        public bool? Hop_Utilization { get; set; }

        #endregion

        internal Equipment() : base()
        {

        }

        /// <summary>
        /// Initializes an Equipment with the given parameters, including boilSize
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="boilSize"></param>
        /// <param name="batchSize"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public Equipment(
            double volume,
            double boilSize,
            double batchSize,
            string name,
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(volume);
            Validation.ValidateGreaterThanZero(boilSize);
            Validation.ValidateGreaterThanZero(batchSize);

            this.Volume = volume;
            this.Boil_Size = boilSize;
            this.Batch_Size = batchSize;
        }

        /// <summary>
        /// Initialized an Equipment with Calc_Bol_Volume = true and the given parameters
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="batchSize"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        public Equipment(
            double volume,
            double batchSize,
            string name,
            double topUpWater,
            double trubChillerLoss,
            double boilTime, 
            double evapRate,
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base(name, version)
        {
            Validation.ValidateGreaterThanZero(volume);
            Validation.ValidateGreaterThanZero(batchSize);

            this.Volume = volume;
            this.Batch_Size = batchSize;

            this.Top_Up_Water = topUpWater;
            this.Trub_Chiller_Loss = trubChillerLoss;
            this.Boil_Time = boilTime;
            this.Evap_Rate = evapRate;

            this.Calc_Boil_Volume = true;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        internal override bool IsValidInternal(ref ValidationCode errorCode)
        {
            bool result = true;

            // Equipment is invalid if the required properties are not set
            // to calculate the boil volume
            if (this.Calc_Boil_Volume.HasValue &&
                this.Calc_Boil_Volume.Value)
            {
                if (this.Top_Up_Water == null ||
                    this.Trub_Chiller_Loss == null ||
                    this.Boil_Time == null ||
                    this.Evap_Rate == null)
                {
                    result = false;
                    errorCode |= ValidationCode.BOIL_VOLUME_REQUIRED_PARAMS_MISSING;
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the boil volume using necessary properties.
        /// </summary>
        /// <returns></returns>
        private double CalculateBoilVolume()
        {
            return ((this.Batch_Size - this.Top_Up_Water - this.Trub_Chiller_Loss) * (1 + this.Boil_Time * this.Evap_Rate)).Value;
        }
    }
}
