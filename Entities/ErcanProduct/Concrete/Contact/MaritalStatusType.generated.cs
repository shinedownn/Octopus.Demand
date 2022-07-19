using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("MaritalStatusType", Schema = "TT_CONTACT")]
    public partial class MaritalStatusType : DomainObjectBaseTT
    {
        [Key]
        public int MaritalStatusTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MaritalStatusTypeResource), ErrorMessageResourceName = "MaritalStatusTypeCodeRequired")]
        [MaxLength(10, ErrorMessageResourceType = typeof(MaritalStatusTypeResource), ErrorMessageResourceName = "MaritalStatusTypeCodeMaxLength")]
        public string MaritalStatusTypeCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MaritalStatusTypeResource), ErrorMessageResourceName = "MaritalStatusTypeNameRequired")]
        [MaxLength(30, ErrorMessageResourceType = typeof(MaritalStatusTypeResource), ErrorMessageResourceName = "MaritalStatusTypeNameMaxLength")]
        public string MaritalStatusTypeName { get; set; }
    }
}
