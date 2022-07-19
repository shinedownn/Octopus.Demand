using Entities.Concrete;
using Entities.Demands.Dtos;
using Entities.Dtos;
using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Dtos;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class NewCallModel
    {
        public IEnumerable<ContactDTO> ContactList { get; set; }

        public FGSIncomingModel FGSIncomingModel { get; set; }
    }
}
