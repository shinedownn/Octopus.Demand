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
    public class HotelRepository : EfEntityRepositoryBase<Hotel, ErcanProductDbContext>, IHotelRepository
    {
        public HotelRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
