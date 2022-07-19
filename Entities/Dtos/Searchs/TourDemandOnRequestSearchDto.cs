using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Searchs
{
    public class TourDemandOnRequestSearchDto : IDto
    {
        public int? MainDemandId { get; set; }
        public int? TourDemandId { get; set; }
        public string Description { get; set; }
        public bool? Approved { get; set; }
        public bool ConfirmationRequested { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Period { get; set; }
        public int TourId { get; set; }
        public string OnRequestName { get; set; }
        public string CreatedUserName { get; set; }
        public string DemandChannel { get; set; }
        public int TourDemandOnRequestId { get; set; }
        public int OnRequestId { get; set; }
        public string WhoApproves { get; set; }
        public string TourOnRequestDescription { get; set; }
        public string ApprovementNote { get; set; }
    }
}
