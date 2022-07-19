using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ErcanProduct;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.ErcanProduct.Concrete.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ErcanProduct
{
    public class NumberRangeRepository : EfEntityRepositoryBase<NumberRange, ErcanProductDbContext>, INumberRangeRepository
    {
        public NumberRangeRepository(ErcanProductDbContext context) : base(context)
        {
        }
    }
}
