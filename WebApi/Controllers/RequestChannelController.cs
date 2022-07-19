 
using Business.Handlers.RequestChannels.Queries;
using Core.Utilities.Results;
using Entities.Dtos.RequestChannel;
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
    public class RequestChannelController : BaseApiController
    {
        ///<summary>
        /// Get All Request Channels
        ///</summary>
        ///<remarks>RequestChannelDto</remarks>
        ///<return>List of RequestChannelDto</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<RequestChannelDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetRequestChannelQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
