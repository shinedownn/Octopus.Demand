using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class RequestChannelDto : IDto
    {
        public int RequestChannelId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ChangedBy { get; set; }
        public int DepartmentId { get; set; }
    }
}
