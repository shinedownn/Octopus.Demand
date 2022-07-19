using Business.Adapters.ContactService;
using Business.Handlers.Demands.Queries;
using Business.Handlers.ErcanProduct.Contact.Queries;
using Business.Helpers;
using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Hubs;
using WebAPI.Models;
using WebAPI.Roles;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FgsController : BaseApiController
    {
        private readonly IHubContext<FgsHub> _hubContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="contactService"></param>
        public FgsController(IHubContext<FgsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// catches incoming call from FGS (Vodafone)
        /// socket uri : url + /WebAPI/fgshub/
        /// </summary> 
        //[AuthorizeRoles(DemandRoles.Read)]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("inComingCall"), HttpPost("inComingCall")]
        //[ApiExplorerSettings(IgnoreApi = true)]

        public async Task<string> inComingCall([FromQuery] FGSIncomingModel vm)
        {
            return await Task.Run<string>(() =>
            {
                if (vm.Event_Type == Event_type.Connected || vm.Event_Type==Event_type.Transferred || vm.Event_Type==Event_type.CallBack)
                {
                    var contactList = (Mediator.Send(new GetContactByPhoneQuery() { Phone = vm.caller })).Result.Data;
                    var newCallModel = new NewCallModel
                    {
                        ContactList = (contactList == null ? new List<ContactDTO>() { new ContactDTO() {
                      DefaultPhoneText=vm.caller,
                      Phones=new List<ContactPhone>()
                      {
                          new ContactPhone(){ FullPhone=vm.caller,Isdefault=true }
                      }
                    } } : contactList),
                        FGSIncomingModel = vm
                    };

                    var json = JsonConvert.SerializeObject(newCallModel, new Newtonsoft.Json.JsonSerializerSettings()
                    {
                        Converters = new List<JsonConverter> {
                            new StringEnumConverter()
                        },
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        Formatting = Formatting.Indented,
                        ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy(),
                        }
                    });
                    _hubContext.Clients.Group(vm.agent).SendAsync("newCall", json).Wait();
                }

                if (vm.Event_Type == Event_type.Ringing)
                {
                    var json = JsonConvert.SerializeObject(new NewCallModel() { FGSIncomingModel=vm }, new Newtonsoft.Json.JsonSerializerSettings()
                    {
                        Converters = new List<JsonConverter> {
                            new StringEnumConverter()
                        },
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        Formatting = Formatting.Indented,
                        ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy(),
                        }
                    });
                    _hubContext.Clients.Group(vm.agent).SendAsync("newCall", json).Wait();
                }
                //Debug.WriteLine(json);
                return "success";
            });
        }
    }
}
