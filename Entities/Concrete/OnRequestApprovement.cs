using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OnRequestApprovement : IEntity
    {
        public int OnRequestApprovementId { get; set; }
        public int OnRequestId { get; set; }
        public int DepartmentId { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
