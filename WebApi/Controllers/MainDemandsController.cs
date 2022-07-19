
using Business.Handlers.Demands.Commands;
using Business.Handlers.Demands.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Entities.Dtos;
using WebAPI.Roles;
using WebAPI.Attributes;
using Core.Utilities.Results;
using Entities.MainDemands.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Demands If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MainDemandsController : BaseApiController
    {
        ///<summary>
        ///List Main Demands
        ///</summary>
        ///<remarks>Demands</remarks>
        ///<return>List Demands</return>
        ///<response code="200"></response>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<MainDemandDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(bool isOpen)
        {
            var result = await Mediator.Send(new GetMainDemandsQuery() { IsOpen=isOpen });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Get Main Demand By Id
        ///</summary>
        ///<remarks>MainDemandDto</remarks>
        ///<return>MainDemandDto</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<MainDemandDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int mainDemandId)
        {
            var result = await Mediator.Send(new GetMainDemandQuery { MainDemandId = mainDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add Main Demand.
        /// </summary>
        /// <param name="createMainDemandCommand"></param>
        /// <returns>Response Message</returns>

        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateMainDemandCommand createMainDemandCommand)
        {
            var result = await Mediator.Send(createMainDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update Main Demand.
        /// </summary>
        /// <param name="updateMainDemandCommand"></param>
        /// <returns>Response Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMainDemandCommand updateMainDemandCommand)
        {
            var result = await Mediator.Send(updateMainDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Main Demand.
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
            var result = await Mediator.Send(new DeleteMainDemandCommand() { MainDemandId= mainDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
