using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{ 
    [Table("Unit", Schema = "COMMON")]
    public partial class Unit : DomainObjectBase
    {

        [Key]
        [DataMember]
        public int UnitId { get; set; }

        [DataMember]
        public string UnitCode { get; set; }

        [DataMember]
        public int UnitTypeId { get; set; }

        [DataMember]
        public bool IsBaseUnit { get; set; }

        [DataMember]
        public int? BaseUnitId { get; set; }

        [DataMember]
        public decimal BaseUnitValue { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public string GlobalCode { get; set; }

    }
}
