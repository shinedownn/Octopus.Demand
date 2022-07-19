 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Business.Handlers.Reminders.Queries;
using Business.Handlers.Reminders.Commands;
using Core.Utilities.Results;
using WebAPI.Roles;
using WebAPI.Attributes;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Reminders If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : BaseApiController
    {
        ///<summary>
        ///List Reminders
        ///</summary>
        ///<remarks>Reminders</remarks>
        ///<return>List Reminders</return>
        ///<response code="200"></response>
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetRemindersQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Reminders</remarks>
        ///<return>Reminders List</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int reminderId)
        {
            var result = await Mediator.Send(new GetReminderQuery { ReminderId = reminderId });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Reminders</remarks>
        ///<return>Reminders List</return>
        ///<response code="200"></response>  
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("getmyreminders")]
        public async Task<IActionResult> GetMyReminders(bool IsActive=true)
        {
            var result = await Mediator.Send(new GetMyRemindersQuery() { IsActive=IsActive});
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Add Reminder.
        /// </summary>
        /// <param name="createReminder"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateReminderCommand createReminder)
        {
            var result = await Mediator.Send(createReminder);
            if (result.Success)
            {
                var reminders = await Mediator.Send(new GetMyRemindersQuery() { IsActive = true });
                return Ok(reminders);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Update Reminder.
        /// </summary>
        /// <param name="updateReminder"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateReminderCommand updateReminder)
        {
            var result = await Mediator.Send(updateReminder);
            if (result.Success)
            {
                var reminders = await Mediator.Send(new GetMyRemindersQuery() { IsActive=true});
                return Ok(reminders);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Delete Reminder.
        /// </summary>
        /// <param name="deleteReminder"></param>
        /// <returns></returns>
        [AuthorizeRoles(DemandRoles.Write)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteReminderCommand deleteReminder)
        {
            var result = await Mediator.Send(deleteReminder);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
