using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainDemandActions.Dtos
{
    public class MainDemandActionDto:IDto
    {
        public int? MainDemandActionId { get; set; } 
        public int ActionId { get; set; }
        public bool IsOpen { get; set; }
        public string Description { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
