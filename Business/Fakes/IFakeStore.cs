using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Fakes
{
    public interface IFakeStore
    {
        List<TEntity> Set<TEntity>();
    }
}
