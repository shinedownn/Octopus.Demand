using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainDemands.Dtos
{
    public class MainDemandUpdateDto : IDto
    {
        public int ContactId { get; set; }
        public int MainDemandId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DemandChannel { get; set; }
        public string ReservationNumber { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFirm { get; set; }
        public string FirmName { get; set; }
        public string FirmTitle { get; set; }
    }
}
