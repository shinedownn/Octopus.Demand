using Core.Entities;
using System.Collections.Generic;

namespace Entities.ErcanProduct.Concrete.Contact
{
    public partial class EmailType : IEntity
    {
        public EmailType()
        {
            Translate = new HashSet<EmailTypeT>();
        }

        public ICollection<EmailTypeT> Translate
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.EmailTypeName;
        }
    }
}
