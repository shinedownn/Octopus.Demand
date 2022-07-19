using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class HotelDto : IDto
    {
        public int HotelId { get; set; }


        public int? ProductId { get; set; }


        public int? HotelClassId { get; set; }


        [MaxLength(-1, ErrorMessage = "")]
        public string MapCode { get; set; }


        public int? MaxChildAge { get; set; }


        public bool IsSingleBay { get; set; }


        public bool IsWithoutWomen { get; set; }


        public bool IsMenWithChild { get; set; }


        public bool IsAllotmentControl { get; set; }


        public bool IsActive { get; set; }


        public bool IsDeleted { get; set; }


        public int HotelLocationId { get; set; }


        public bool? CheckChild { get; set; }


        [MaxLength(50, ErrorMessage = "")]
        public string Latitude { get; set; }


        [MaxLength(50, ErrorMessage = "")]
        public string Longitude { get; set; }

        public int? Volume { get; set; }

        public decimal? Priority { get; set; }

        public decimal? Doping { get; set; }

        [MaxLength(500, ErrorMessage = "")]
        public string VirtualTourLink { get; set; }


        public int? CountryId { get; set; }


        public int? CityId { get; set; }


        public int? DistrictId { get; set; }


        [MaxLength(500, ErrorMessage = "")]
        public string Address { get; set; }


        [MaxLength(500, ErrorMessage = "")]
        public string WebSite { get; set; }


        public int? PaximumId { get; set; }

        public int? GiataCode { get; set; }

        public bool? IsEBinsurance { get; set; }

        public bool? IsChannelManager { get; set; }

        public int? ChannelManagerId { get; set; }

        public string XmlDescription { get; set; }

        public string ChildDescription { get; set; }

        public bool IsMatchHotelTSejour { get; set; }

        public string BookingAnalyticCode { get; set; }

        public string HotelBookingLogo { get; set; }

        public DateTime? HotelOpenDate { get; set; }
        public DateTime? HotelCloseDate { get; set; }
        public bool? IsHotelOpenInYear { get; set; }
        public bool? IsCityHotel { get; set; }

        public string HotelOpenText { get; set; }

        public string HotelBookingGroupKey { get; set; }

        public string CategorySliderPath { get; set; }

        public bool? IsInsuranceExist { get; set; } 
        public bool? IsUnderDesk { get; set; }

        public int? ChildAge1Min { get; set; }

        public int? ChildAge1Max { get; set; }

        public int? ChildAge2Min { get; set; }

        public int? ChildAge2Max { get; set; }

        public bool IsAdultHotel { get; set; }

        public int? MinimumAcceptedAge { get; set; }
        public bool? IsContract { get; set; }

        public bool? IsHoliday { get; set; }
        public bool? IsResort { get; set; }

        public bool IsMultipleChannel { get; set; }
        public int? ContractType { get; set; }

        public string PuzzleRefId { get; set; }
        public string TegSoftCampaignId { get; set; }

        public bool? IsVilla { get; set; }
    }
}
