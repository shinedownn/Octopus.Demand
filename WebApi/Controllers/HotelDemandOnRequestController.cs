using Business.Handlers.HotelDemandOnRequests.Commands;
using Business.Handlers.HotelDemandOnRequests.Queries;
using Core.Utilities.Results;
using Entities.Dtos.HotelDemandOnRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelDemandOnRequestController : BaseApiController
    {
        ///<summary>
        /// Get Hotel Demand OnRequest By Id
        ///</summary>
        ///<remarks>HotelDemandOnRequest</remarks>
        ///<return>HotelDemandOnRequestDto</return>
        ///<response code="200">HotelDemandOnRequestDto</response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<HotelDemandOnRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int hotelDemandOnRequestId)
        {
            var result = await Mediator.Send(new GetHotelDemandOnRequestQuery { HotelDemandOnRequestId = hotelDemandOnRequestId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Get Hotel Demand OnRequests By HotelDemandId
        ///</summary>
        ///<remarks>HotelDemandOnRequests</remarks>
        ///<return>List of HotelDemandOnRequestDto</return>
        ///<response code="200">List of HotelDemandOnRequestDto</response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<HotelDemandOnRequestDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByHotelDemandId")]
        public async Task<IActionResult> GetByHotelDemandId(int hotelDemandId)
        {
            var result = await Mediator.Send(new GetHotelDemandOnRequestsQuery { HotelDemandId = hotelDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Create HotelDemandOnRequest
        /// </summary>
        /// <param name="createHotelDemandOnRequestCommand"></param>
        /// <returns>Result Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateHotelDemandOnRequestCommand createHotelDemandOnRequestCommand)
        {
            var result = await Mediator.Send(createHotelDemandOnRequestCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Update HotelDemandOnRequest
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>Result Message</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateHotelDemandOnRequestCommand updateHotelDemandOnRequestCommand)
        {
            var result = await Mediator.Send(updateHotelDemandOnRequestCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Delete HotelDemandOnRequest
        ///</summary>
        ///<remarks>HotelDemandOnRequest</remarks>
        ///<return>Result Message</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int hotelDemandOnRequestId)
        {
            var result = await Mediator.Send(new DeleteHotelDemandOnRequestCommand() { HotelDemandOnRequestId = hotelDemandOnRequestId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
