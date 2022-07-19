using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract; 

namespace DataAccess.Concrete.EntityFramework
{
    public class MainDemandActionRepository : EfEntityRepositoryBase<MainDemandAction, ProjectDbContext>, IMainDemandActionRepository
    {
        public MainDemandActionRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}

