using System;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    public abstract class BaseTranslateEntity :BaseEntity
    {
        public int LanguageId
        {
            get;
            set;
        }

    }
}
