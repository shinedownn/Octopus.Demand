using Business.Handlers.Actions.Commands;
using Business.Handlers.Actions.Enums;
using Business.Handlers.Actions.Queries;
using Core.Utilities.Results;
using Entities.Actions.Dtos;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
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
    public class ActionsController : BaseApiController
    {
        ///<summary>
        /// Get All Actions
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>ActionDto</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<ActionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetActionsQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Get Action By Id
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>ActionDto</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<ActionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int actionId)
        { 
           var result = await Mediator.Send(new GetActionQuery { ActionId = actionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Get Action By Type
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>ActionDto</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<ActionDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getByType")]
        public async Task<IActionResult> GetByType(ActionTypeEnum actionType,bool IsOpen)
        {
            var result = await Mediator.Send(new GetActionsByTypeQuery {  actionTypeEnum = actionType, IsOpen=IsOpen });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Create Action
        /// </summary>
        /// <param name="createActionCommand"></param>
        /// <returns>Result Message</returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateActionCommand createActionCommand)
        {
            var result=await Mediator.Send(createActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Update Action
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>Result Message</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateActionCommand updateActionCommand)
        {
            var result = await Mediator.Send(updateActionCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        /// Delete Action
        ///</summary>
        ///<remarks>Action</remarks>
        ///<return>Result Message</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int actionId)
        {
            var result = await Mediator.Send(new DeleteActionCommand() { ActionId=actionId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
