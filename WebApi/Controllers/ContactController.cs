using Business.Handlers.Contacts.Queries;
using Business.Handlers.ErcanProduct.CallCenters.Queries;
using Core.Utilities.Results;
using Entities.ErcanProduct.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseApiController
    {
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<ContactDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))] 
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int contactId)
        {
            
            var result = await Mediator.Send(new GetContactByIdQuery() { ContactId = contactId });
            if (result.Success) return new ContentResult {
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(result, new JsonSerializerSettings {
                    Converters = new List<JsonConverter> {
                            new StringEnumConverter()
                        },
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy(),
                    }
                }),
                //ContentEncoding = Encoding.UTF8
            };
            return BadRequest(result);
        }
        [AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<IEnumerable<object>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("GetCallCenterPersonels")]
        public async Task<IActionResult> GetCallCenterPersonels()
        {
            var result = await Mediator.Send(new GetCallCenterPersonelsQuery());
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);                
        }
    }
}
