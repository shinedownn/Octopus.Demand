using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.TourDemandOnRequests
{
    public class TourDemandOnRequestInsertDto : IDto
    {
        public int OnRequestId { get; set; }
        public string Description { get; set; } 
        public int? AskingForApprovalDepartmentId { get; set; }  //onay isteyen departman id
        public bool ConfirmationRequested { get; set; }          //onay istendi mi?
        public int? ApprovalRequestedDepartmentId { get; set; }  // onay istenen departman id
        public bool? Approved { get; set; }                      // onaylandı mı? 
        public int? ApprovingDepartmentId { get; set; }           // onaylayan departman id
        public string WhoApproves { get; set; }                  // kim onayladı?
        public DateTime? ApprovedDate { get; set; }              // onaylanma tarihi
        public bool IsOpen { get; set; }
    }
}
