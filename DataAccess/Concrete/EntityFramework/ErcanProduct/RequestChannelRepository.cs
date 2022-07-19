using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ErcanProduct;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.ErcanProduct.Concrete.CallCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ErcanProduct
{
    public class RequestChannelRepository : EfEntityRepositoryBase<RequestChannel, ErcanProductDbContext>, IRequestChannelRepository
    {
        public RequestChannelRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
