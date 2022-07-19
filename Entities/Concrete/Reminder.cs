using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Reminder : IEntity
    {
        public int ReminderId { get; set; }
        public int? HotelDemandActionId { get; set; }
        public int? TourDemandActionId { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
