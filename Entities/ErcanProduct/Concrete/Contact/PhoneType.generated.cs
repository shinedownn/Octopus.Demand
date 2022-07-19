using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("PhoneType", Schema = "TT_CONTACT")]
    public partial class PhoneType : DomainObjectBaseTT
    {
        [Key]
        public int PhoneTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(PhoneTypeResource), ErrorMessageResourceName = "PhoneTypeCodeRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(PhoneTypeResource), ErrorMessageResourceName = "PhoneTypeCodeMaxLength")]
        public string PhoneTypeCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(PhoneTypeResource), ErrorMessageResourceName = "PhoneTypeNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(PhoneTypeResource), ErrorMessageResourceName = "PhoneTypeNameMaxLength")]
        public string PhoneTypeName { get; set; }


    }
}
