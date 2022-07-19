using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.OnRequests
{
    public class OnRequestDto : IDto
    {
        public int OnRequestId { get; set; }
        public string Name { get; set; } 
        public bool IsDeleted { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
