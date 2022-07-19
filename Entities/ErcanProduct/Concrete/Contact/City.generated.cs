using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{

    [Table("City", Schema = "COMMON")]
    public partial class City : DomainObjectBase
    {
        [Key]
        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public string IataCode { get; set; }

        [DataMember]
        public string PlateCode { get; set; }

        [DataMember]
        public bool IsDelete { get; set; }

        [DataMember]
        public string CityName { get; set; }

        [DataMember]
        public int? StateId { get; set; }

        [DataMember]
        public int? CityZoneId { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }
    }
}
