using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [DataContract, Serializable]
    public partial class District  : IEntity
    {
        public District()
        {
            Translate = new HashSet<DistrictT>();
        }

        [DataMember]
        public ICollection<DistrictT> Translate
        {
            get;
            set;
        }
        [DataMember]
        [ForeignKey("CityId")]
        public City City { get; set; }

        public override string ToString()
        {
            return DistrictName;
        }
    }
}
