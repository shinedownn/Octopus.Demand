using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("AddressType", Schema = "TT_CONTACT")]
    public partial class AddressType : DomainObjectBaseTT
    {
        [Key]
        public int AddressTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AddressTypeResource), ErrorMessageResourceName = "AddressTypeCodeRequired")]
        [MaxLength(10, ErrorMessageResourceType = typeof(AddressTypeResource), ErrorMessageResourceName = "AddressTypeCodeMaxLength")]
        public string AddressTypeCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AddressTypeResource), ErrorMessageResourceName = "AddressTypeNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(AddressTypeResource), ErrorMessageResourceName = "AddressTypeNameMaxLength")]
        public string AddressTypeName { get; set; }
    }
}
