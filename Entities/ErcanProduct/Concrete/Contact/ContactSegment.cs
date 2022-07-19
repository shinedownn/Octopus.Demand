using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [DataContract]
    public partial class ContactSegment : IEntity
    {
        [ForeignKey("SegmentTypeId")]
        [DataMember]
        public ContactSegmentType ContactSegmentType { get; set; }
    }
}
