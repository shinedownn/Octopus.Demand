using Core.Entities;
using Entities.ErcanProduct.Concrete.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    public partial class ContactAddress : IEntity
    {

        public ContactAddress()
        {
            Country = new Country();
            City = new City();
            District = new District();
        }
        [ForeignKey("CountryId"), DataMember]
        public Country Country { get; set; }

        [ForeignKey("CityId"), DataMember]
        public City City { get; set; }
       
        [ForeignKey("DistrictId"), DataMember]
        public District District { get; set; }

        [NotMapped,DataMember]
        public string AddressTypeName
        {
            get
            {
                if (AddressTypeId!=null)
                {
                    return Enum.GetName(typeof(AddressTypeEnum), AddressTypeId);
                }
                else
                {
                    return "";
                }
            }
            set { }
        }
        [NotMapped, DataMember]
        public int? StateId { get; set; }

        [NotMapped, DataMember]
        public string Guid { get; set; }

        [NotMapped, DataMember]
        public string CountryName { get; set; }

        [NotMapped, DataMember]
        public string CityName { get; set; }

        [NotMapped, DataMember]
        public string DistrictName { get; set; }
        public Contact Contact { get; set; }

        

    }
}
