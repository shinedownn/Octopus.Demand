using Business.Handlers.MainDemandActions.Commands;
using Business.Handlers.MainDemandActions.Queries;
using Core.Utilities.Results;
using Entities.Actions.Dtos;
using Entities.Dtos;
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
    public class MainDemandActionsController : BaseApiController
    {
        /// <summary>
        /// Get Main Demand Action By Id
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
            var result = await Mediator.Send(new GetMainDemandActionQuery { MainDemandActionId = actionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get Main Demand Actions By MainDemandId
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <response code="200"></response>
        /// <param name="mainDemandId"></param>
        /// <returns>Demand</returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<ActionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByMainDemandId")]

        public async Task<IActionResult> GetByMainDemandId(int mainDemandId)
        {
            var result = await Mediator.Send(new GetMainDemandActionsQuery { MainDemandId = mainDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }          

        /// <summary>
        /// Create Main Demand Action
        /// </summary>
        /// <param name="createMainDemandActionCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]

        public async Task<IActionResult> Update([FromBody] CreateMainDemandActionCommand createMainDemandActionCommand)
        {
            var result = await Mediator.Send(createMainDemandActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        /// <summary>
        /// Update Main Demand Action
        /// </summary>
        /// <param name="updateMainDemandActionCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] UpdateMainDemandActionCommand updateMainDemandActionCommand)
        {
            var result = await Mediator.Send(updateMainDemandActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Action Item
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]

        public async Task<IActionResult> Delete(int actionId)
        {
            var result = await Mediator.Send(new DeleteMainDemandActionCommand { ActionId = actionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
