using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("EmailType", Schema = "TT_CONTACT")]
    public partial class EmailType : DomainObjectBaseTT
    {
        [Key]
        public int EmailTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(EmailTypeResource), ErrorMessageResourceName = "EmailTypeCodeRequired")]
        [MaxLength(10, ErrorMessageResourceType = typeof(EmailTypeResource), ErrorMessageResourceName = "EmailTypeCodeMaxLength")]
        public string EmailTypeCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(EmailTypeResource), ErrorMessageResourceName = "EmailTypeNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(EmailTypeResource), ErrorMessageResourceName = "EmailTypeNameMaxLength")]
        public string EmailTypeName { get; set; }
    }
}
