using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class TourDemandRepository : EfEntityRepositoryBase<TourDemand,ProjectDbContext>,ITourDemandRepository
    {
        public TourDemandRepository(ProjectDbContext context):base(context)
        {

        }
    }
}
