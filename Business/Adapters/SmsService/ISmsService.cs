using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters.SmsService
{
    public interface ISmsService
    {
        Task<bool> Send(string password, string text, string cellPhone);
        Task<bool> SendAssist(string text, string cellPhone);
    }
}
