using AutoMapper;
using Consul;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Entities.Dtos;
using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters.ContactService
{
    public class ContactServiceManager : IContactService
    {
        private IConfiguration _configuration;
        private IMapper _mapper;

        public ContactServiceManager(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper; 
        }
        public async Task<List<ContactDTO>> GetContactByPhone(string phone)
        {
            //var configurationManager = ServiceTool.ServiceProvider.GetService<Business.ConfigurationManager>(); 
            string ocelotUri = _configuration.GetSection("ocelot:host").Value;  

            var accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://{ocelotUri}/contact/Contact/getContactByPhone?phone=" + (phone.IndexOf("+") > -1 ? phone.Replace("+",""):phone));
            client.DefaultRequestHeaders.Add("Authorization", new String[] { accessor.HttpContext.Request.Headers["Authorization"] });
            var response = await client.GetAsync(client.BaseAddress, new System.Threading.CancellationToken());
            if ((int)response.StatusCode == StatusCodes.Status200OK)
            {
                var contactstr = await response.Content.ReadAsStringAsync();
                var contact = JsonConvert.DeserializeObject<ApiResult<List<ContactDTO>>>(contactstr);
                var mapp= _mapper.Map<List<ContactDTO>>(contact.Data);
                return mapp;
            }
            return new List<ContactDTO>() { new ContactDTO() { Phones = new List<ContactPhone>() { new ContactPhone() { FullPhone = phone } } } };
        }
    }
}
