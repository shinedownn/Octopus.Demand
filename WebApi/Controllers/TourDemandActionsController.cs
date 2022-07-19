using Business.Handlers.TourDemandActions.Commands;
using Business.Handlers.TourDemandActions.Queries;
using Core.Utilities.Results;
using Entities.Actions.Dtos;
using Entities.Dtos;
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
    public class TourDemandActionsController : BaseApiController
    {
        /// <summary>
        /// Get Tour Action By Id
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <response code="200"></response>
        /// <param name="actionId"></param>
        /// <returns>Demand</returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<ActionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]

        public async Task<IActionResult> GetById(int actionId)
        {
            var result = await Mediator.Send(new GetTourDemandActionQuery { TourDemandActionId = actionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Get Tour Actions 
        /// </summary> 
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<ActionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByTourDemandId")]
        public async Task<IActionResult> GetAll(int tourDemandId)
        {
            var result = await Mediator.Send(new GetTourDemandActionsQuery() { TourDemandId = tourDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Create Actions 
        /// </summary> 
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateTourDemandActionCommand createTourDemandActionCommand)
        {
            var result = await Mediator.Send(createTourDemandActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        
        /// <summary>
        /// Update Action Item
        /// </summary>
        /// <param name="updateTourDemandActionCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] UpdateTourDemandActionCommand updateTourDemandActionCommand)
        {
            var result = await Mediator.Send(updateTourDemandActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Delete Action Item
        /// </summary>
        /// <param name="TourActionId"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]

        public async Task<IActionResult> Delete(int TourActionId)
        {
            var result = await Mediator.Send(new DeleteTourDemandActionCommand { ActionId = TourActionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
