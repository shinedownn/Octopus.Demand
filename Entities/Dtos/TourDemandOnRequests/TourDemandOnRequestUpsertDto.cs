using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.TourDemandOnRequests
{
    public class TourDemandOnRequestUpsertDto : IDto
    {
        public int? TourDemandOnRequestId { get; set; }
        public int? MainDemandId { get; set; }
        public int? TourDemandId { get; set; }
        public int OnRequestId { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public bool? Approved { get; set; }
        public bool ConfirmationRequested { get; set; }
        public int? ApprovalRequestedDepartmentId { get; set; }
        public int? AskingForApprovalDepartmentId { get; set; } 
        public int? ApprovingDepartmentId { get; set; }           // onaylayan departman id
        public string WhoApproves { get; set; }                  // kim onayladı?
        public DateTime? ApprovedDate { get; set; }              // onaylanma tarihi
    }
}
