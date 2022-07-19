using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Department : IEntity
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedUserName { get; set; }
    }
}
