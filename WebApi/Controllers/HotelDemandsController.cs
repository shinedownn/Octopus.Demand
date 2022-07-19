using Business.Handlers.HotelDemands.Commands;
using Business.Handlers.HotelDemands.Queries;
using Core.Utilities.Results;
using Entities.Dtos;
using Entities.HotelDemands.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class HotelDemandsController : BaseApiController
    {
        ///<summary>
        /// Get Hotel Demand By Id
        ///</summary>
        ///<remarks>Demands</remarks>
        ///<return>Demand List</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<HotelDemandDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int hotelDemandId)
        {
            //var handler = new JwtSecurityTokenHandler();
            //string authHeader = Request.Headers["Authorization"];
            //authHeader = authHeader.Replace("Bearer ", "");
            //var jsonToken = handler.ReadToken(authHeader);
            //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            //var id = tokenS.Claims.First(claim => claim.Type == "name").Value;

            var result = await Mediator.Send(new GetHotelDemandQuery { HotelDemandId = hotelDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add Hotel Demand.
        /// </summary>
        /// <param name="createHotelDemandCommand"></param>
        /// <returns>Response Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateHotelDemandCommand createHotelDemandCommand)
        {
            var result = await Mediator.Send(createHotelDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update Demand.
        /// </summary>
        /// <param name="updateHotelDemandCommand"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateHotelDemandCommand updateHotelDemandCommand)
        {
            var result = await Mediator.Send(updateHotelDemandCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Demand.
        /// </summary>
        /// <param name="hotelDemandId"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int hotelDemandId)
        {
            var result = await Mediator.Send(new DeleteHotelDemandCommand { HotelDemandId=hotelDemandId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
