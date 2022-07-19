using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class TourDetail: IDto
    {
        public int TourId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int TourTypeId { get; set; }
        public string RouteInfo { get; set; }
        public string ShortSummary { get; set; }
        public bool IsActive { get; set; }
        public List<TourAvailablePeriod> TourAvailablePeriods { get; set; }
    }


    public class TourAvailablePeriod 
    {
        public int TourId { get; set; }
        public int TourPeriodId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PeriodDate { get; set; }
        public int Id { get; set; }
    }
}
