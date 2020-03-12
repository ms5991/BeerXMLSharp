using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM.Records
{
    public class Hop : BeerXMLRecordBase
    {
        #region Required Properties

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Alpha { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Weight { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public HopUse Use { get; set; }

        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public double Time { get; set; }

        #endregion

        #region Optional Properties

        [BeerXMLInclude()]
        public HopType? Type { get; set; }

        [BeerXMLInclude()]
        public HopForm? Form { get; set; }

        [BeerXMLInclude()]
        public double? Beta { get; set; }

        [BeerXMLInclude()]
        public double? HSI { get; set; }

        [BeerXMLInclude()]
        public string Origin { get; set; }

        [BeerXMLInclude()]
        public string Substitutes { get; set; }

        [BeerXMLInclude()]
        public double? Humulene { get; set; }

        [BeerXMLInclude()]
        public double? Caryophyllene { get; set; }

        [BeerXMLInclude()]
        public double? Cohumulone { get; set; }

        [BeerXMLInclude()]
        public double? Myrcene { get; set; }

        #endregion

        #region Extension properties

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Display_Amount { get; set; }

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Inventory { get; set; }

        [BeerXMLInclude(PropertyRequirement.OPTIONAL, isExtension: true)]
        public string Display_Time { get; set; }

        #endregion

        internal Hop() 
        {

        }

        public Hop(
            string name, 
            double alpha, 
            double weightInKg, 
            HopUse use, 
            int timeInMin, 
            int version = Constants.DEFAULT_BEER_XML_VERSION) : base (name, version)
        {
            Validation.ValidatePercentileRange(alpha);
            Validation.ValidateGreaterThanZero(weightInKg);
            Validation.ValidateGreaterThanZero(timeInMin);

            this.Alpha = alpha;
            this.Weight = weightInKg;
            this.Use = use;
            this.Time = timeInMin;
        }
    }
}
