using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    
    public partial class ContactSegmentType : IEntity
    {
        public ContactSegmentType()
        {
            ContactSegments = new HashSet<ContactSegment>();
        }

        [ForeignKey("ContactSegmentId")]
        public ICollection<ContactSegment> ContactSegments { get; set; }
    }
}
