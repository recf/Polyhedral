using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Polyhedral
{
    public abstract class Die<T> : IDie<T>
    {
        #region Implementation of IDie<TResult>

        public uint Faces { get; }

        public abstract T Transform(uint value);

        public IList<T> GetResultSpace()
        {
            var sides = (int) Faces;
            var space = Enumerable.Range(1, sides).Select(x => Transform((uint)x));

            return space.ToList();
        }

        #endregion

        protected Die(uint faces)
        {
            Faces = faces;
        }
    }

    public class Die : Die<uint>
    {
        public Die(uint faces) : base (faces) { }

        #region Overrides of Die<uint>

        public override uint Transform(uint value)
        {
            return value;
        }

        #endregion
    }
}
