using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyhedral
{
    // TODO Add d20. Copied from Rotted Capes which doesn't really use d20s
    public class DiceRoll : IEquatable<DiceRoll>
    {
        public static DiceRoll Parse(string input)
        {
            var dice = new Dictionary<int, uint>()
                           {
                               { 4, 0 },
                               { 6, 0 },
                               { 8, 0 },
                               { 10, 0 },
                               { 12, 0 },
                           };

            var modifier = 0;

            var unconsumed = input;
            while (unconsumed.Length > 0)
            {
                var posD = unconsumed.IndexOf('d');

                if (posD >= 0)
                {
                    var posNextToken = unconsumed.IndexOfAny("+-".ToCharArray(), 1);
                    if (posNextToken < 0)
                    {
                        posNextToken = unconsumed.Length;
                    }

                    var numString = unconsumed.Substring(0, posD);
                    var sizeString = unconsumed.Substring(posD + 1, posNextToken - posD - 1);

                    var num = numString == string.Empty || numString == "+" ? (uint)1 : uint.Parse(numString);

                    var size = int.Parse(sizeString);

                    dice[size] = num;

                    unconsumed = posNextToken == unconsumed.Length ? string.Empty : unconsumed.Substring(posNextToken);
                }
                else
                {
                    modifier = int.Parse(unconsumed);
                    unconsumed = string.Empty;
                }
            }

            return new DiceRoll(d4: dice[4], d6: dice[6], d8: dice[8], d10: dice[10], d12: dice[12], modifier: modifier);
        }

        public static bool operator ==(DiceRoll a, DiceRoll b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.D4 == b.D4
                && a.D6 == b.D6
                && a.D8 == b.D8
                && a.D10 == b.D10
                && a.D12 == b.D12
                && a.Modifier == b.Modifier;
        }

        public static bool operator !=(DiceRoll a, DiceRoll b)
        {
            return !(a == b);
        }

        private Dictionary<int, uint> _storage;

        public uint D4
        {
            get
            {
                return _storage[4];
            }
        }

        public uint D6
        {
            get
            {
                return _storage[6];
            }
        }

        public uint D8
        {
            get
            {
                return _storage[8];
            }
        }

        public uint D10
        {
            get
            {
                return _storage[10];
            }
        }

        public uint D12
        {
            get
            {
                return _storage[12];
            }
        }

        public int Modifier { get; private set; }

        public int MaximumRoll
        {
            get
            {
                return _storage.Aggregate(Modifier, (acc, kvp) => acc + (kvp.Key * (int)kvp.Value));
            }
        }

        public DiceRoll(uint d4 = 0, uint d6 = 0, uint d8 = 0, uint d10 = 0, uint d12 = 0, int modifier = 0)
        {
            _storage = new Dictionary<int, uint>()
                           {
                               { 4, d4 },
                               { 6, d6 },
                               { 8, d8 },
                               { 10, d10 },
                               { 12, d12 },
                           };
            this.Modifier = modifier;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            var firstOne = true;
            var nonZeroDice = from kvp in _storage where kvp.Value > 0 orderby kvp.Key descending select kvp;
            foreach (var kvp in nonZeroDice)
            {
                if (!firstOne)
                {
                    builder.Append("+");
                }

                firstOne = false;

                if (kvp.Value > 1)
                {
                    builder.Append(kvp.Value);
                }

                builder.AppendFormat("d{0}", kvp.Key);
            }

            if (Modifier != 0 || !nonZeroDice.Any())
            {
                builder.AppendFormat("{0:+0;-0}", this.Modifier);
            }

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            var d = obj as DiceRoll;

            return Equals(d);
        }

        public override int GetHashCode()
        {
            return MaximumRoll;
        }

        public bool Equals(DiceRoll other)
        {
            if (other == null)
            {
                return false;
            }

            return D4 == other.D4
                && D6 == other.D6
                && D8 == other.D8
                && D10 == other.D10
                && D12 == other.D12
                && Modifier == other.Modifier;
        }
    }
}
