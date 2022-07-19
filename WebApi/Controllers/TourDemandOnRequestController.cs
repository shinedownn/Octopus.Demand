using Business.Handlers.TourDemandOnRequests.Commands;
using Business.Handlers.TourDemandOnRequests.Queries;
using Core.Utilities.Results;
using Entities.Dtos.TourDemandOnRequests;
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
    public class TourDemandOnRequestController : BaseApiController
    {
        ///<summary>
        /// Get Tour Demand OnRequest By Id
        ///</summary>
        ///<remarks>TourDemandOnRequest</remarks>
        ///<return>TourDemandOnRequestDto</return>
        ///<response code="200">TourDemandOnRequestDto</response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<TourDemandOnRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int tourDemandOnRequestId)
        {
            var result = await Mediator.Send(new GetTourDemandOnRequestQuery { TourDemandOnRequestId = tourDemandOnRequestId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Get Tour Demand OnRequests By TourDemandId
        ///</summary>
        ///<remarks>TourDemandOnRequests</remarks>
        ///<return>List of TourDemandOnRequestDto</return>
        ///<response code="200">List of TourDemandOnRequestDto</response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<TourDemandOnRequestDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByTourDemandId")]
        public async Task<IActionResult> GetByTourDemandId(int tourDemandId)
        {
            var result = await Mediator.Send(new GetTourDemandOnRequestsQuery { TourDemandId = tourDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Create TourDemandOnRequest
        /// </summary>
        /// <param name="createTourDemandOnRequestCommand"></param>
        /// <returns>Result Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateTourDemandOnRequestCommand createTourDemandOnRequestCommand)
        {
            var result = await Mediator.Send(createTourDemandOnRequestCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Update TourDemandOnRequest
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>Result Message</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateTourDemandOnRequestCommand updateTourDemandOnRequestCommand)
        {
            var result = await Mediator.Send(updateTourDemandOnRequestCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Delete TourDemandOnRequest
        ///</summary>
        ///<remarks>TourDemandOnRequest</remarks>
        ///<return>Result Message</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int tourDemandOnRequestId)
        {
            var result = await Mediator.Send(new DeleteTourDemandOnRequestCommand() { TourDemandOnRequestId = tourDemandOnRequestId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
