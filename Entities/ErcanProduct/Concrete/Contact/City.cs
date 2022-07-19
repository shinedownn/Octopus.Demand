using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [DataContract, Serializable]
    public partial class City  
    {
        public City()
        {
            Translate = new HashSet<CityT>();
           
         
        }

        [DataMember]
        public ICollection<CityT> Translate
        {
            get;
            set;
        }

        [ForeignKey("CountryId")]
        [DataMember]
        public Country Country { get; set; }

        public override string ToString()
        {
            return CityName;
        }

        [NotMapped]
        public int FlightCampaignRuleItemId { get; set; }
    }
}
