using Core.Entities;
using Core.Utilities.Results;
using Entities.Dtos.HotelDemandOnRequests;
using Entities.Dtos.TourDemandOnRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Searchs
{
    public class SearchOnRequestsDto : IDto
    {
        public PagingResult<HotelDemandOnRequestSearchDto> hotelDemandOnRequestSearchDto { get; set; }
        public PagingResult<TourDemandOnRequestSearchDto> tourDemandOnRequestSearchDto { get; set; }
        public int AllTotalItemCount { get; set; }
    }
}
