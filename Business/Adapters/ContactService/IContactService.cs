using Entities.Dtos;
using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters.ContactService
{
    public interface IContactService
    {
        Task<List<ContactDTO>> GetContactByPhone(string phone);
    }
}
