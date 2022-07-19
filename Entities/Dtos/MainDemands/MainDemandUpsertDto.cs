﻿using Core.Entities;
using Entities.Dtos.HotelDemands;
using Entities.Dtos.TourDemands;
using Entities.MainDemandActions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MainDemands
{
    public class MainDemandUpsertDto : IDto
    {
        public int? MainDemandId { get; set; }
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DemandChannel { get; set; }
        public string ReservationNumber { get; set; }
        public bool IsFirm { get; set; }
        public string FirmName { get; set; }
        public string FirmTitle { get; set; }
        public bool IsOpen { get; set; }
        public List<MainDemandActionDto> Actions { get; set; } = new();
        public List<HotelDemandUpsertDto> HotelDemandUpsertDtos { get; set; } = new();
        public List<TourDemandUpsertDto> TourDemandUpsertDtos { get; set; } = new();
    }
}
