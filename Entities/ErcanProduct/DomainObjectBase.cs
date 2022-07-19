using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct
{
    [DataContract, Serializable]
    public class DomainObjectBase  
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
     
    public class DomainObjectBaseTT  
    {

    }
}
