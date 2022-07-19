
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DepartmentRepository : EfEntityRepositoryBase<Department, ProjectDbContext>, IDepartmentRepository
    {
        public DepartmentRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
