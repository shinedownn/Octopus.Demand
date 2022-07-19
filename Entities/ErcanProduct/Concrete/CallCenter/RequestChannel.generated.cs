using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.CallCenter
{
    [Table("RequestChannel", Schema = "CALLCENTER")]
    public partial class RequestChannel : IEntity
    { 
        [Key]
        public int RequestChannelId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } 
        public int DepartmentId { get; set; }
    }
}
