using Business.Helpers;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalRSwaggerGen.Attributes;
using SignalRSwaggerGen.Enums;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    [SignalRHub] 
    public class FgsHub : Hub
    {
        private readonly IHubContext<FgsHub> _hubContext; 
        public FgsHub(IHubContext<FgsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [SignalRMethod(summary: "method1 summary", description: "method1 description", autoDiscover: AutoDiscover.Params)]
        public override async Task OnConnectedAsync()
        { 
            var email = GetEmailFromToken(); 
            await Groups.AddToGroupAsync(Context.ConnectionId, email.ToString()); 
            await base.OnConnectedAsync(); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var email = GetEmailFromToken();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, email);
            await base.OnDisconnectedAsync(exception);
        } 

        private string GetEmailFromToken()
        {
            var accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            var handler = new JwtSecurityTokenHandler(); 
            string authQuery = accessor.HttpContext.Request.Query["access_token"].FirstOrDefault().Replace("Bearer ", ""); 
            var jsonToken = handler.ReadToken(authQuery);
            var tokenS = handler.ReadToken(authQuery) as JwtSecurityToken;
            var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            return email;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="vm"></param>
        //[SignalRMethod(summary: "Sending Fgs data", description: "Catch data from Fgs (Vodafone) with SignalR frontend method name newCall", autoDiscover: AutoDiscover.Params)]
        //public void newCall(NewCallModel newCallModel)
        //{
        //    var userId = JwtHelper.GetValue("magnus_id").ToString();
        //    _hubContext.Clients.Group(userId).SendAsync("newCall", newCallModel).Wait();
        //}
    }
}
