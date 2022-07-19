using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DemandEventLog : IDto
    {
        public int EventLogId { get; set; }
        public string Description { get; set; }
        public DateTime SaveDate { get; set; }
        public string CreatedUserName { get; set; }

    }
}
