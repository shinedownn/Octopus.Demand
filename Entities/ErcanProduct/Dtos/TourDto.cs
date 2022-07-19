using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class TourDto : IDto
    {
        public int TourId { get; set; }
        public int? TourTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductId { get; set; }
        public bool IsSingleBay { get; set; }
        public bool IsWithoutWomen { get; set; }
        public bool IsMenWithChild { get; set; }
        public int? MaxChildAge { get; set; }
        public bool IsWithoutVisa { get; set; }
        public bool IsWithDomesticGuide { get; set; }
        public bool IsWithForeignGuide { get; set; }
        public bool IsWithFlight { get; set; }
        public bool? IsIncludedPriceExtraTour { get; set; }
        [MaxLength(11, ErrorMessage = "")]
        public string Tourcode { get; set; }
        public string CountryInfo { get; set; }
        public string TourProgram { get; set; }
        public string Route { get; set; }
        public string TransportionInfo { get; set; }
        public string ShipInfo { get; set; }
        public string RouteInfo { get; set; }
        public int? TransportationId { get; set; }
        public string CategorySliderPath { get; set; }
        public string RoutePicturePath { get; set; }
        public bool? IsFestivalContent { get; set; }
        public string FestivalContent { get; set; }
        public int? TourDays { get; set; }
        public string DeckPlanPicturePath { get; set; }
        public int? OrderId { get; set; }
        public int? Volume { get; set; }
        public bool? IsStandartCost { get; set; }
    }
}
