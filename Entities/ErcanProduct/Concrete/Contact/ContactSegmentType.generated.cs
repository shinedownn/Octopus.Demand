using Entities.Concrete.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactSegmentType", Schema = "CONTACT")] 
    public partial class ContactSegmentType : DomainObjectBase
    {
        [Key, DataMember]
        public int SegmentTypeId { get; set; }
        [DataMember]
        public string SegmentName { get; set; }
        [DataMember]
        public string ColorCode { get; set; }

        [DataMember]
        public bool IsShoppingSegment { get; set; }

    }
}
