using Core.Entities;
using System.Collections.Generic;

namespace Entities.ErcanProduct.Concrete.Contact
{
    public partial class AddressType : IEntity
    {

        public AddressType()
        {
            Translate = new HashSet<AddressTypeT>();
        }

       

        public ICollection<AddressTypeT> Translate
        {
            get; set; }
    }
}
