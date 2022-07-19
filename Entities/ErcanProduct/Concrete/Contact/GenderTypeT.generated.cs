using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("GenderType_T", Schema = "CONTACT")]
    public partial class GenderTypeT : BaseTranslateEntity
    {
        [Key]
        public int GenderTypeTId { get; set; }
        public int GenderTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(GenderTypeResource), ErrorMessageResourceName = "GenderNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(GenderTypeResource), ErrorMessageResourceName = "GenderNameMaxLength")]
        public int GenderName { get; set; }
    }
}
