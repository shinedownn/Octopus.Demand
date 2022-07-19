using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Entities.Concrete.Action;

namespace DataAccess.Concrete.EntityFramework
{
    public class ActionRepository : EfEntityRepositoryBase<Action, ProjectDbContext>, IActionRepository
    {
        public ActionRepository(ProjectDbContext context):base(context)
        {

        }
    }
}
