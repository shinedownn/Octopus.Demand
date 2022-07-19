﻿using Core.Entities;
using Entities.Dtos.HotelDemandOnRequests;
using Entities.HotelDemandActions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.HotelDemands.Dtos
{
    public class HotelDemandUpdateDto : IDto
    {
        public int HotelDemandId { get; set; }
        public int MainDemandId { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; } 
        public List<HotelDemandActionUpdateDto> Actions { get; set; } = new();
        public List<HotelDemandOnRequestUpdateDto> Requests { get; set; } = new();
    }
}
