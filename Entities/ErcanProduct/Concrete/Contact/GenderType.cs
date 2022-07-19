using Core.Entities;
using System.Collections.Generic;

namespace Entities.ErcanProduct.Concrete.Contact
{
    public partial class GenderType : IEntity
    {
        public GenderType()
        {
            Translate = new HashSet<GenderTypeT>();
        }

        public ICollection<GenderTypeT> Translate
        {
            get;
            set;
        }
    }
}
