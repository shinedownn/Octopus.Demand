 
using Entities.ErcanProduct.Concrete.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    public abstract class BaseEntity
    {
        [NotMapped]
        private ItemState _state = ItemState.NotChanged;

        [NotMapped]
        public ItemState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
    }


}
