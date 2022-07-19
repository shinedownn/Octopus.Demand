using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Searchs
{
    public class HotelDemandOnRequestSearchDto : IDto
    {
        public int? MainDemandId { get; set; }
        public int? HotelDemandId { get; set; }
        public string Description { get; set; }
        public bool? Approved { get; set; }
        public bool ConfirmationRequested { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int HotelId { get; set; }
        public string OnRequestName { get; set; }
        public string CreatedUserName { get; set; }
        public string DemandChannel { get; set; }
        public int? HotelDemandOnRequestId { get; set; }
        public int OnRequestId { get; set; }
        public string WhoApproves { get; set; }
        public string HotelOnRequestDescription { get; set; }
        public string ApprovementNote { get; set; } 
    }
}
