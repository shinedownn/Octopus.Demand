
using Business.Handlers.OnRequestApprovements.Commands;
using Business.Handlers.OnRequestApprovements.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Utilities.Results;
using WebAPI.Attributes;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    /// <summary>
    /// OnRequestApprovements If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OnRequestApprovementsController : BaseApiController
    {
        ///<summary>
        ///List OnRequestApprovements
        ///</summary>
        ///<remarks>OnRequestApprovements</remarks>
        ///<return>List OnRequestApprovements</return>
        ///<response code="200"></response>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OnRequestApprovement>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOnRequestApprovementsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>OnRequestApprovements</remarks>
        ///<return>OnRequestApprovements List</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OnRequestApprovement))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int onRequestApprovementId)
        {
            var result = await Mediator.Send(new GetOnRequestApprovementQuery { OnRequestApprovementId = onRequestApprovementId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get Approvement By OnRequestId
        /// </summary>
        /// <param name="onRequestId"></param>
        /// <returns></returns> 
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Department>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByOnRequestId")]
        public async Task<IActionResult> GetByOnRequestId(int onRequestId)
        {
            var result = await Mediator.Send(new GetOnRequestApprovementByOnRequestIdQuery { OnRequestId = onRequestId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add OnRequestApprovement.
        /// </summary>
        /// <param name="createOnRequestApprovement"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOnRequestApprovementCommand createOnRequestApprovement)
        {
            var result = await Mediator.Send(createOnRequestApprovement);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update OnRequestApprovement.
        /// </summary>
        /// <param name="updateOnRequestApprovement"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOnRequestApprovementCommand updateOnRequestApprovement)
        {
            var result = await Mediator.Send(updateOnRequestApprovement);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete OnRequestApprovement.
        /// </summary>
        /// <param name="deleteOnRequestApprovement"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOnRequestApprovementCommand deleteOnRequestApprovement)
        {
            var result = await Mediator.Send(deleteOnRequestApprovement);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }
    }
}
