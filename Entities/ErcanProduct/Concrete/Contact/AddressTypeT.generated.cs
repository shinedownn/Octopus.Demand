using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("AddressType_T", Schema = "CONTACT")]
    public partial class AddressTypeT : BaseTranslateEntity
    {
        [Key]
        public int AddressTypeTId { get; set; }
        public int AddressTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AddressTypeResource), ErrorMessageResourceName = "AddressTypeNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(AddressTypeResource), ErrorMessageResourceName = "AddressTypeNameMaxLength")]
        public string AddressTypeName { get; set; }
    }
}
