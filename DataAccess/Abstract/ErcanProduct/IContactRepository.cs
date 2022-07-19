using Core.DataAccess;
using Entities.ErcanProduct.Concrete.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.ErcanProduct
{
    public interface IContactRepository :IEntityRepository<Contact>
    {
    }
}
