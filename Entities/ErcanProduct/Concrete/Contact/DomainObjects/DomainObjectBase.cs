using Entities.ErcanProduct.Concrete.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DomainObjects
{
    [Serializable]
    [DataContract]
    public class DomainObjectBase : BaseEntity
    {
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public int? ChangedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? ChangedDate { get; set; }
    }

    [Serializable]
    public class DomainObjectBaseTT : BaseEntity
    {

    }
}
