using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("GenderType", Schema = "TT_CONTACT")]
    public partial class GenderType:DomainObjectBaseTT
    {
        [Key]
        public int GenderTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(GenderTypeResource), ErrorMessageResourceName = "GenderCodeRequired")]
        [MaxLength(1, ErrorMessageResourceType = typeof(GenderTypeResource), ErrorMessageResourceName = "GenderCodeMaxLength")]
        public string GenderCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(GenderTypeResource), ErrorMessageResourceName = "GenderNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(GenderTypeResource), ErrorMessageResourceName = "GenderNameMaxLength")]
        public string GenderName { get; set; }
    }
}
