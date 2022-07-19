using Entities.Concrete.DomainObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactSegment", Schema = "CONTACT"),Serializable]
    public partial class ContactSegment : DomainObjectBase
    {
        [Key, DataMember]
        public int ContactSegmentId { get; set; }
        [DataMember]
        public int ContactId { get; set; }
        [DataMember]
        public int SegmentTypeId { get; set; }

    }
}
