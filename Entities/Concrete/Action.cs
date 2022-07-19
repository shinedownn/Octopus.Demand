using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Action : IEntity
    {
        public int ActionId { get; set; } 
        public string ActionType { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; } 
        public bool IsDeleted { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
