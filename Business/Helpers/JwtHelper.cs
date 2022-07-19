using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class JwtHelper
    {
        public static object GetValue(string key)
        { 
            var accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); 
            var obj= accessor.HttpContext.User.Claims.Where(x => x.Type == key).FirstOrDefault().Value;
            return obj;

            //var handler = new JwtSecurityTokenHandler();
            //string authHeader = accessor.HttpContext.Request.Headers["Authorization"];
            //string authQuery = accessor.HttpContext.Request.Query["Authorization"];
            //authHeader = !string.IsNullOrEmpty(authHeader)? authHeader.Replace("Bearer ", ""): authQuery.Replace("Bearer ", "");
            //var jsonToken = handler.ReadToken(authHeader);
            //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            //var value = tokenS.Claims.First(claim => claim.Type == key).Value;
            //return value;
        }
    }
}
