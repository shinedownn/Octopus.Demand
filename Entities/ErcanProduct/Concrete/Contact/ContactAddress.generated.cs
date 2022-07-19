using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactAddress", Schema = "CONTACT"),DataContract]
    public partial class ContactAddress : DomainObjectBase
    {
        [Key,DataMember] 
        public int AddressId { get; set; }
        [DataMember]
        public int ContactId { get; set; }
        [DataMember]
        public int? AddressTypeId { get; set; }
        [DataMember]
        public bool IsDefault { get; set; }
        [DataMember]
        public int CountryId { get; set; }
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public int? DistrictId { get; set; }
        [DataMember]
        public int? LocationId { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longtitude { get; set; }
        [DataMember]
        public string MapCode { get; set; }
        [DataMember]
        public bool? IsInvoice { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ContactAddressResource), ErrorMessageResourceName = "AddressRequired")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ContactAddressResource), ErrorMessageResourceName = "AddressMaxLength")]
        public string Address { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
