using Core.Entities;
using System.Collections.Generic;

namespace Entities.ErcanProduct.Concrete.Contact
{
    public partial class Discourse : IEntity
    {
        public Discourse()
        {
            Translate = new HashSet<DiscourseT>();
        }



        public ICollection<DiscourseT> Translate
        {
            get;
            set;
        }
    }
}
