using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.IdentityServer
{
    public class IdentityServerTokenOptions 
    {
        public string TokenUrl { get; set; }
        public string AuthorizeUrl { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public string AuthorityUrl { get; set; }
    }
}
