using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.System
{
    public partial class NumberRange : IEntity
    {
        //[ForeignKey("ModuleUINameId")]
        //public ModuleUIName ModuleUIName { get; set; }

        public string Unique
        {
            get { return Prefix + Value; }
        }
    }
}
