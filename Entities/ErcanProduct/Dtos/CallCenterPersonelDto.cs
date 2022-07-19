using Core.Entities;
using Entities.ErcanProduct.Concrete.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class CallCenterPersonelDto : IDto
    {
        public CallCenterPersonelDto()
        {
            Contact = new Contact();
        }

        [Key]
        public int CallCenterPersonelId { get; set; }
        public int ContactId { get; set; }
        public int? CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public int? CallCenterGroupId { get; set; }
        [StringLength(150)]
        public string TegSoftUserId { get; set; }
        public int? RoleTypeId { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("CallCenterPersonels")]
        public virtual Contact Contact { get; set; }
    }
}
