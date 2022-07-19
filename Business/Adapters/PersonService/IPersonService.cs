using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters.PersonService
{
    public interface IPersonService
    {
        Task<bool> VerifyCid(Citizen citizen);
    }
}
