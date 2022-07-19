﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.HotelDemandActions.Dtos
{
    public class HotelDemandActionInsertDto : IDto
    { 
        public int ActionId { get; set; }
        public string Description { get; set; }
    }
}
