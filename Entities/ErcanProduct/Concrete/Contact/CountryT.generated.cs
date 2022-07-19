using Entities.Concrete.DomainObjects.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("Country_T", Schema = "COMMON")]
    public partial class CountryT : BaseTranslateEntity
    {
        public CountryT()
        {

        }

        [Key]
        public int CountryTId { get; set; }

        public int CountryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(CountryResource), ErrorMessageResourceName = "CountryNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(CountryResource), ErrorMessageResourceName = "CountryNameMaxLength")]
        public string CountryName { get; set; }

    }
}
