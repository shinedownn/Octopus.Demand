using Business.Handlers.TourDemands.Commands;
using Business.Handlers.TourDemands.Queries;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.TourDemands.Dtos;
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
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TourDemandsController : BaseApiController
    {
        ///<summary>
        /// Get Tour Demand By Id
        ///</summary> 
        ///<return>Tour Demand</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<TourDemandDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int tourDemandId)
        {
            var result = await Mediator.Send(new GetTourDemandQuery { TourDemandId = tourDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Get Tour Demands By MainDemandId
        /// </summary>
        /// <param name="mainDemandId"></param>
        /// <returns>Tour Demands List</returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<TourDemandDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByMainDemandId")]
        public async Task<IActionResult> GetByMainDemandId(int mainDemandId)
        {
            var result = await Mediator.Send(new GetTourDemandsQuery() { MainDemandId= mainDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        

        /// <summary>
        /// Add Tour Demand.
        /// </summary>
        /// <param name="createTourDemandCommand"></param>
        /// <returns>Response Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateTourDemandCommand createTourDemandCommand)
        {
            var result = await Mediator.Send(createTourDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update Tour Demand.
        /// </summary>
        /// <param name="updateTourDemandCommand"></param>
        /// <returns>Response Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTourDemandCommand updateTourDemandCommand)
        {
            var result = await Mediator.Send(updateTourDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Tour Demand.
        /// </summary>
        /// <param name="tourDemandId"></param>
        /// <returns>Response Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int tourDemandId)
        {
            var result = await Mediator.Send(new DeleteTourDemandCommand { TourDemandId= tourDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
