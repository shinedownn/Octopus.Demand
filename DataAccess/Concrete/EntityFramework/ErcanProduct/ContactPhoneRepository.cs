using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ErcanProduct;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.ErcanProduct.Concrete.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ErcanProduct
{
    public class ContactPhoneRepository : EfEntityRepositoryBase<ContactPhone, ErcanProductDbContext>, IContactPhoneRepository
    {
        public ContactPhoneRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
