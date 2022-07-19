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
    public class TourPermaLinkRepository : EfEntityRepositoryBase<TourPermaLink, ErcanProductDbContext>, ITourPermaLinkRepository
    {
        public TourPermaLinkRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
