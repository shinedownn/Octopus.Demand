using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactPhone", Schema = "CONTACT"),DataContract]
    public partial class ContactPhone : DomainObjectBase
    {
        [Key,DataMember]
        public int ContactPhoneId { get; set; }
        [DataMember]
        public int ContactId { get; set; }
        [DataMember]
        public int? PhoneTypeId { get; set; }
        [DataMember]
        public bool Isdefault { get; set; }

        [DataMember]
        [MaxLength(20, ErrorMessageResourceType = typeof(ContactPhoneResource), ErrorMessageResourceName = "PhoneMaxLength")]
        //[RegularExpression(@"^((((\(\d{3}\))|(\d{3}-))\d{3}-\d{4})|(\+?\d{2}((-| )\d{1,8}){1,5}))(( x| ext)\d{1,5}){0,1}$"
        //    , ErrorMessageResourceType = typeof(ContactPhoneResource), ErrorMessageResourceName = "InvalidPhoneFormat")]
        public string Phone { get; set; }
        [DataMember]
        public string FullPhone { get; set; }
        [DataMember]
        public string InternalPhone { get; set; }
        [DataMember]
        public int? Line { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string CountryPhoneCode { get; set; }
        [DataMember]
        public string PhoneCode { get; set; }
        [DataMember]
        public bool? IsPhoneApproved { get; set; }
        [DataMember]
        public bool? IsApprovedForMasterPass { get; set; }
    }
}
