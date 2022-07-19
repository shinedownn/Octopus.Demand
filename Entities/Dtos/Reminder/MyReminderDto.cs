using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Reminder
{
    public class MyReminderDto
    {
        public int ReminderId { get; set; }
        public int? MainDemandId { get; set; }
        public int? HotelDemandId { get; set; }
        public int? TourDemandId { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public string HotelName { get; set; }
        public string TourName { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public int? HotelDemandActionId { get; set; }
        public int? TourDemandActionId { get; set; }
    }
}
