using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class HotelDemandOnRequest : IEntity
    { 
        public int? HotelDemandOnRequestId { get; set; }
        public int? MainDemandId { get; set; }
        public int? HotelDemandId { get; set; }
        public int OnRequestId { get; set; }
        public int? AskingForApprovalDepartmentId { get; set; }  //onay isteyen departman id
        public bool ConfirmationRequested { get; set; }          //onay istendi mi?
        public int? ApprovalRequestedDepartmentId { get; set; }  // onay istenen departman id
        public bool? Approved { get; set; }                      // onaylandı mı? 
        public int? ApprovingDepartmentId { get; set; }           // onaylayan departman id
        public string WhoApproves { get; set; }                  // kim onayladı?
        public DateTime? ApprovedDate { get; set; }              // onaylanma tarihi
        public string Note { get; set; }                         // onay / red notu
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
