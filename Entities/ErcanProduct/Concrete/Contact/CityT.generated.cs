using Entities.Concrete.DomainObjects.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("City_T", Schema = "COMMON")]
    public partial class CityT  
    {
        public CityT()
        {

        }

        [Key]
        public int CityTId { get; set; }

        public int CityId { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(CityResource), ErrorMessageResourceName = "CityNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(CityResource), ErrorMessageResourceName = "CityNameMaxLength")]
        public string CityName { get; set; }
        
    }
}
