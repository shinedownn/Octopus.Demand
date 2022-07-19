using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RequestChannel : IEntity
    { 
        public int RequestChannelId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public int DepartmentId { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
