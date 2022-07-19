using Business.Handlers.Demands.Commands;
using Business.Handlers.Demands.Queries;
using Core.Utilities.Results;
using Entities.Demands.Dtos;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///  
    [Route("api/[controller]")]
    [ApiController]
    public class DemandsController : BaseApiController
    {
        /// <summary>
        /// Get Demand By Id
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <response code="200"></response>
        /// <param name="mainDemandId">Main Demand Id</param>
        /// <returns>Demand</returns>
        [AuthorizeRoles(DemandRoles.Read)]       
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<DemandsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]

        public async Task<IActionResult> GetById(int mainDemandId) 
        {
            var result = await Mediator.Send(new GetDemandQuery { MainDemandId = mainDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Add Demand
        /// </summary>
        /// <remarks>Demands</remarks> 
        /// <param name="createDemandCommand">DemandsDto</param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<DemandsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]

        public async Task<IActionResult> Add([FromBody] CreateDemandCommand createDemandCommand)
        {
            var result = await Mediator.Send(createDemandCommand);
            if (result.Success)
            {
                var demand = await Mediator.Send(new GetDemandQuery { MainDemandId = (int)result.Data });

                return Ok(demand);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update Demand
        /// </summary>
        /// <param name="updateDemandCommand">UpdateDemandCommand</param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]

        public async Task<IActionResult> Update([FromBody] UpdateDemandCommand updateDemandCommand)
        {
            var result = await Mediator.Send(updateDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Upsert Demand
        /// </summary>
        /// <param name="upsertDemandCommand"></param>
        /// <returns>Response Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("upsert")]
        public async Task<ActionResult> Upsert(UpsertDemandCommand upsertDemandCommand)
        {
            var result = await Mediator.Send(upsertDemandCommand);
            if (result.Success)
            {
                var demand = await Mediator.Send(new GetDemandQuery { MainDemandId = (int)result.Data });

                return Ok(demand); 
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Demand
        /// </summary>
        /// <param name="mainDemandId"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]

        public async Task<IActionResult> Delete(int mainDemandId)
        {
            var result = await Mediator.Send(new DeleteDemandCommand { MainDemandId=mainDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
