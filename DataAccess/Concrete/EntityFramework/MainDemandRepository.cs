
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class MainDemandRepository : EfEntityRepositoryBase<MainDemand, ProjectDbContext>, IMainDemandRepository
    {
        public MainDemandRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
