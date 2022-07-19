using Business.Handlers.HotelDemandActions.Commands;
using Business.Handlers.HotelDemandActions.Queries;
using Core.Utilities.Results;
using Entities.Dtos;
using Entities.HotelDemandActions.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Define Actions for Hotel Demand
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelDemandActionsController : BaseApiController
    {
        /// <summary>
        /// Get Hotel Action By Id
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <response code="200"></response>
        /// <param name="hotelDemandActionId"></param>
        /// <returns>Demand</returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<HotelDemandActionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]

        public async Task<IActionResult> GetById(int hotelDemandActionId)
        {
            var result = await Mediator.Send(new GetHotelDemandActionQuery { HotelDemandActionId = hotelDemandActionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get Hotel Actions 
        /// </summary> 
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<HotelDemandActionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByHotelDemandId")]
        public async Task<IActionResult> GetByHotelDemandId(int hotelDemandId)
        {
            var result = await Mediator.Send(new GetHotelDemandActionsQuery() { HotelDemandId= hotelDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Add Hotel Action
        /// </summary>
        /// <param name="createHotelActionCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]

        public async Task<IActionResult> Add([FromBody] CreateHotelDemandActionCommand createHotelActionCommand)
        {
            var result = await Mediator.Send(createHotelActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        /// <summary>
        /// Update Hotel Action
        /// </summary>
        /// <param name="updateHotelDemandActionCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] UpdateHotelDemandActionCommand updateHotelDemandActionCommand)
        {
            var result = await Mediator.Send(updateHotelDemandActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Hotel Action
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
            var result = await Mediator.Send(new DeleteHotelDemandActionCommand { ActionId = actionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
