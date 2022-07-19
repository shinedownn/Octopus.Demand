using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    /// <summary>
    /// LogAspect
    /// </summary>
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _description;
        public LogAspect(Type loggerService,string description="")
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _description = description;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //_loggerServiceBase?.Info(GetLogDetail(invocation));
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            var user = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "name").Value;
            //_loggerServiceBase?.Info(user+" "+ _description); 
            _loggerServiceBase?.Info(GetLogDetail(invocation)); 
        }
        private string GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,  
                });
            }
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                User = (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User.Claims == null) ? "?" : _httpContextAccessor.HttpContext.User.Identity.Name,
                Description = _description
            };
            return JsonConvert.SerializeObject(logDetail);
        }
         
    }
}
