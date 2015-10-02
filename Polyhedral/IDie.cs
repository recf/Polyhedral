using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyhedral
{
    public interface IDie<T>
    {
        uint Faces { get; }

        IList<T> GetResultSpace();
    }
}
