using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("MaritalStatusType_T", Schema = "CONTACT")]
    public partial class MaritalStatusTypeT : BaseTranslateEntity
    {
        [Key]
        public int MaritalStatusTypeTId { get; set; }

        public int MaritalStatusTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MaritalStatusTypeResource), ErrorMessageResourceName = "MaritalStatusTypeNameRequired")]
        [MaxLength(30, ErrorMessageResourceType = typeof(MaritalStatusTypeResource), ErrorMessageResourceName = "MaritalStatusTypeNameMaxLength")]
        public string MaritalStatusTypeName { get; set; }
    }
}
