using Business.Handlers.ErcanProduct.Tour.Queries;
using Business.Handlers.ErcanProduct.Tour.TourPeriods.Queries; 
using Business.Handlers.ToursFromElasticSearch.Queries;
using Core.Utilities.ElasticSearch.Models;
using Core.Utilities.Results;
using Entities.ErcanProduct.Concrete.Tour;
using Entities.ErcanProduct.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : BaseApiController
    {
        /// <summary>
        /// List of Tour Names
        /// </summary>
        /// <returns></returns> 
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<List<ElasticSearchGetModel<Entities.Concrete.ElasticSearchModel>>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("TourSearchAutoComplete")]
        public async Task<IActionResult> HotelSearchAutoComplete(string query)
        {
            var result = await Mediator.Send(new GetToursAutoCompleteQuery { TourName = query });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get Tour Permalink
        /// </summary>
        /// <returns></returns> 
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IDataResult<TourPermalinkDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getPermaLinkByTourId")]
        public async Task<IActionResult> getPermaLinkByTourId(int tourId)
        {
            var result = await Mediator.Send(new GetPermaLinkByTourIdQuery {  TourId= tourId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get TourId By ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tuple<int, string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getTourIdByProductId")]
        public async Task<IActionResult> getTourIdByProductId(int productId)
        {
            var result = await Mediator.Send(new GetTourIdByProductIdQuery { ProductId = productId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get Tour Periods By ProductId
        /// </summary>
        /// <returns></returns> 
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<TourAvailablePeriod>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getTourPeriods")]
        public async Task<IActionResult> getTourPeriods(int tourId)
        {
            var result = await Mediator.Send(new GetTourPeriods { TourId = tourId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
