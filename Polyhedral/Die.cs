using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Polyhedral
{
    public class Die<T> : IDie<T>
    {
        private readonly Func<uint, T> _transform;

        #region Implementation of IDie<TResult>

        public uint Faces { get; }

        public IList<T> GetResultSpace()
        {
            var sides = (int) Faces;
            var space = Enumerable.Range(1, sides).Select(x => _transform((uint)x));

            return space.ToList();
        }

        #endregion

        public Die(uint faces, Func<uint, T> transform)
        {
            Faces = faces;
            _transform = transform;
        }
    }

    public class Die : Die<uint>
    {
        public Die(uint faces) : base (faces, u => u) { }
    }
}
