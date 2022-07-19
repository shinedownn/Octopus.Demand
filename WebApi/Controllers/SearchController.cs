
using Business.Handlers.Searchs.Queries;
using Core.Utilities.Results;
using Entities.Dtos;
using Entities.Dtos.Searchs;
using Entities.MainDemands.Dtos;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : BaseApiController
    {
        /// <summary>
        /// Search Main Demand
        /// </summary>
        /// <returns></returns> 
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<MainDemandDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("search")]
        public async Task<IActionResult> Index([FromQuery]GetDemandsSearchQuery getDemandsSearchQuery)
        {
            var result = await Mediator.Send(getDemandsSearchQuery);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Search On Requests
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<SearchOnRequestsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getOnRequests")]

        public async Task<IActionResult> GetOnRequests([FromQuery]GetOnRequestsSearchQuery getOnRequestsSearchQuery)
        {
            var result = await Mediator.Send(getOnRequestsSearchQuery);
            if(result.Success) return Ok(result);
            return BadRequest(result); 
        }

        /// <summary>
        /// Search On Requests
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<SearchOnRequestsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getOnRequestsByDepartment")]

        public async Task<IActionResult> GetOnRequestsByDepartment([FromQuery] GetOnRequestsSearchByDepartmentQuery getOnRequestsSearchByDepartmentQuery)
        {

            var result = await Mediator.Send(getOnRequestsSearchByDepartmentQuery);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Search Main Demand By ContactId
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<MainDemandDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getMainDemandByContactId")]

        public async Task<IActionResult> GetMainDemandByContactId([FromQuery] GetDemandsSearchByContactIdQuery getDemandsSearchByContactIdQuery)
        { 
            var result = await Mediator.Send(getDemandsSearchByContactIdQuery);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Search On Requests By ContactId
        /// </summary>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<SearchOnRequestsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getOnRequestsByContactId")]

        public async Task<IActionResult> GetOnRequestsByContactId([FromQuery] GetOnRequestsSearchByContactIdQuery getOnRequestsSearchByContactId)
        {
            var result = await Mediator.Send(getOnRequestsSearchByContactId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    } 
}
