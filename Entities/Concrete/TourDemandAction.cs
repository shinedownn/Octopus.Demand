using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TourDemandAction : IEntity
    { 
        public int? TourDemandActionId { get; set; } 
        public int MainDemandId { get; set; }
        public int TourDemandId { get; set; }
        public string Description { get; set; }
        public int ActionId { get; set; }
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
