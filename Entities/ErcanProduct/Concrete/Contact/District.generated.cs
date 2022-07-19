using Entities.Concrete.DomainObjects;
using Entities.Concrete.DomainObjects.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("District", Schema = "COMMON")]
    public partial class District : DomainObjectBase
    {
        [Key]
        [DataMember]
        public int DistrictId { get; set; }

        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public bool IsDelete { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DistrictResource), ErrorMessageResourceName = "DistrictNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(DistrictResource), ErrorMessageResourceName = "DistrictNameMaxLength")]
        public string DistrictName { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

    }
}
