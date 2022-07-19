using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ErcanProduct;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.ErcanProduct.Concrete.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ErcanProduct
{
    public class HotelPermaLinkRepository : EfEntityRepositoryBase<HotelPermaLink, ErcanProductDbContext>, IHotelPermaLinkRepository
    {
        public HotelPermaLinkRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
