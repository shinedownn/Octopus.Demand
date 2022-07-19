using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("PhoneType_T", Schema = "CONTACT")]
    public partial class PhoneTypeT : BaseTranslateEntity
    {
        [Key]
        public int PhoneTypeTId { get; set; }
        public int PhoneTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(PhoneTypeResource), ErrorMessageResourceName = "PhoneTypeNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(PhoneTypeResource), ErrorMessageResourceName = "PhoneTypeNameMaxLength")]
        public int PhoneTypeName { get; set; }

    }
}
