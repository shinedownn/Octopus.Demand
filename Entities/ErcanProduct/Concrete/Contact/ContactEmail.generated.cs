using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactEmail", Schema = "CONTACT")]
    [DataContract]
    public partial class ContactEmail : DomainObjectBase
    {
        [Key]
        [DataMember]
        public int ContactEmailId { get; set; }

        [DataMember]
        public int ContactId { get; set; }
        [DataMember]
        public int EmailTypeId { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ContactEmailResource), ErrorMessageResourceName = "EmailRequired")]
        //[MaxLength(50, ErrorMessageResourceType = typeof(ContactEmailResource), ErrorMessageResourceName = "EmailMaxLength")]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
        //    , ErrorMessageResourceType = typeof(ContactEmailResource), ErrorMessageResourceName = "InvalidEmailFormat")]
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool IsDefault { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
