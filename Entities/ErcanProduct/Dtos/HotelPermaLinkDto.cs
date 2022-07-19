using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class HotelPermaLinkDto : IDto
    {
        public int HotelPermaLinkId { get; set; }
        public int HotelId { get; set; }
        public int LanguageId { get; set; }
        public string Link { get; set; }
    }
}
