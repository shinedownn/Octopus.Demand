using Core.Entities;
using System.Collections.Generic;

namespace Entities.ErcanProduct.Concrete.Contact
{
    public partial class PhoneType : IEntity
    {
        public PhoneType()
        {
            Translate = new HashSet<PhoneTypeT>();
        }


        public ICollection<PhoneTypeT> Translate
        {
            get;
            set;
        }
    }
}
