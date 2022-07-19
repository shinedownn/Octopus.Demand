using Core.Entities;
using System.Collections.Generic;

namespace Entities.ErcanProduct.Concrete.Contact
{
    public partial class MaritalStatusType : IEntity
    {
        public MaritalStatusType()
        {
            Translate = new HashSet<MaritalStatusTypeT>();
        }

        public ICollection<MaritalStatusTypeT> Translate
        {
            get;
            set;
        }
    }
}
