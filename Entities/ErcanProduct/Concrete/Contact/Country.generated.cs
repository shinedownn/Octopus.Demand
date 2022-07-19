using Core.Entities;
using Entities.Concrete.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{

    [Table("Country", Schema = "COMMON")]
    public partial class Country :DomainObjectBase
    {
        [Key]
        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        public bool IsDelete { get; set; }

        [DataMember]
        public int? UnitIdMoney { get; set; }

        [DataMember]
        public string PhoneCode { get; set; }
    }
}
