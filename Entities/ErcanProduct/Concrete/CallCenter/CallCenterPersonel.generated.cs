using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Entities.ErcanProduct.Concrete.CallCenter
{
    [Table("CallCenterPersonel", Schema = "CALLCENTER")]
    public partial class CallCenterPersonel : DomainObjectBase
    {
        [Key]
        public int CallCenterPersonelId { get; set; }
        public int ContactId { get; set; }
        public int? CompanyId { get; set; }
        public bool IsDeleted { get; set; }  
        public int? CallCenterGroupId { get; set; }
        [StringLength(150)]
        public string TegSoftUserId { get; set; }
        public int? RoleTypeId { get; set; }

        //[ForeignKey(nameof(ContactId))]
        //[InverseProperty("CallCenterPersonels")]
        //public virtual Entities.ErcanProduct.Concrete.Contact.Contact Contact { get; set; }
    }
}
