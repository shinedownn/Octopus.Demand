using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.ErcanProduct
{
    public interface ITourPermaLinkRepository : IEntityRepository<Entities.ErcanProduct.Concrete.Tour.TourPermaLink>
    {
    }
}
