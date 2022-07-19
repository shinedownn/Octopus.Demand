using Business.Handlers.OnRequests.Commands;
using Business.Handlers.OnRequests.Queries;
using Core.Utilities.Results;
using Entities.Dtos.OnRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class OnRequestsController : BaseApiController
    {
        /// <summary>
        /// Get OnRequests
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <response code="200"></response> 
        /// <returns>Demand</returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<OnRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getAll")]

        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetOnRequestsQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        /// <summary>
        /// Get OnRequest By Id
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <response code="200"></response>
        /// <param name="onRequestId"></param>
        /// <returns>Demand</returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<OnRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]

        public async Task<IActionResult> GetById(int onRequestId)
        {
            var result = await Mediator.Send(new GetOnRequestQuery { OnRequestId = onRequestId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add OnRequest
        /// </summary>
        /// <param name="createOnRequestCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]

        public async Task<IActionResult> Add([FromBody] CreateOnRequestCommand createOnRequestCommand)
        {
            var result = await Mediator.Send(createOnRequestCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update OnRequest 
        /// </summary>
        /// <param name="updateOnRequestCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] UpdateOnRequestCommand updateOnRequestCommand)
        {
            var result = await Mediator.Send(updateOnRequestCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete OnRequest 
        /// </summary>
        /// <param name="onRequestId"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]

        public async Task<IActionResult> Delete(int onRequestId)
        {
            var result = await Mediator.Send(new DeleteOnRequestCommand {  OnRequestId = onRequestId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
