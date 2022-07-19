using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ErcanProduct;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.ErcanProduct.Concrete.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ErcanProduct
{
    public class TourPeriodRepository : EfEntityRepositoryBase<TourPeriod, ErcanProductDbContext>, ITourPeriodRepository
    {
        public TourPeriodRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
