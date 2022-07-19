using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TourDemand : IEntity
    {
        public int TourDemandId { get; set; } 
        public int MainDemandId { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Period { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int TotalCount { get; set; }
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
