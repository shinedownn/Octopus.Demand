using Entities.Concrete.DomainObjects.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("District_T", Schema = "COMMON")]
    public partial class DistrictT : BaseTranslateEntity
    {
        [Key]
        public int DistrictTId { get; set; }

        public int DistrictId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DistrictResource), ErrorMessageResourceName = "DistrictNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(DistrictResource), ErrorMessageResourceName = "DistrictNameMaxLength")]
        public string DistrictName { get; set; }

    }
}
