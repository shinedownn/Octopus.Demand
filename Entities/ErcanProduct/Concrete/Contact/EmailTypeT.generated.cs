using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("EmailType_T", Schema = "CONTACT")]
    public partial class EmailTypeT : BaseTranslateEntity
    {
        [Key]
        public int EmailTypeTId { get; set; }

        public int EmailTypeId { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(EmailTypeResource), ErrorMessageResourceName = "EmailTypeNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(EmailTypeResource), ErrorMessageResourceName = "EmailTypeNameMaxLength")]
        public string EmailTypeName { get; set; }

    }
}
