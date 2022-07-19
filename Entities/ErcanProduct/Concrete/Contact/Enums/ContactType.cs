using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Enums
{
    [Serializable]
    [DataContract]
    public enum ContactType
    {
        [EnumMember]
        Kisi = 1,
        [EnumMember]
        Firma = 2, 
    }
}
