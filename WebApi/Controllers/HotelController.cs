using Business.Handlers.ErcanProduct.Hotel.Queries;
using Business.Handlers.HotelsFromElasticSearch.Queries;
using Core.Utilities.ElasticSearch.Models;
using Core.Utilities.Results;
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
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : BaseApiController
    {
        /// <summary>
        /// List of Hotel Names
        /// </summary>
        /// <returns></returns> 
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<List<ElasticSearchGetModel<Entities.Concrete.ElasticSearchModel>>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("HotelSearchAutoComplete")]
        public async Task<IActionResult> HotelSearchAutoComplete(string query)
        {
            var result = await Mediator.Send(new GetHotelsAutoCompleteQuery { HotelName=query });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get HotelId By ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<Tuple<int, string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getHotelIdByProductId")]
        public async Task<IActionResult> getHotelIdByProductId(int productId)
        {
            var result = await Mediator.Send(new GetHotelIdByProductIdQuery { ProductId = productId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Get PermaLink By HotelId
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getPermaLinkByHotelId")]
        public async Task<IActionResult> getPermaLinkByHotelId(int hotelId)
        {
            var result = await Mediator.Send(new GetPermaLinkByHotelIdQuery { HotelId = hotelId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
