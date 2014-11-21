using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Polyhedral.Tests
{
    [TestFixture]
    public class DiceRollTests
    {
        public object[][] StringRepresentationCases
        {
            get
            {
                var cases = new List<object[]>
                {
                    new object[] { new DiceRoll(), "+0" },
                    new object[] { new DiceRoll(d4: 1), "d4" },
                    new object[] { new DiceRoll(d6: 1), "d6" },
                    new object[] { new DiceRoll(d8: 1), "d8" },
                    new object[] { new DiceRoll(d10: 1), "d10" },
                    new object[] { new DiceRoll(d12: 1), "d12" },
                    new object[] { new DiceRoll(d4: 1, d12: 1), "d12+d4" },
                    new object[] { new DiceRoll(d4: 2, d12: 2), "2d12+2d4" },
                    new object[] { new DiceRoll(modifier: 3), "+3" },
                    new object[] { new DiceRoll(modifier: -1), "-1" },
                    new object[] { new DiceRoll(d10: 2), "2d10" },
                    new object[] { new DiceRoll(d8: 1, modifier: 2), "d8+2" },
                    new object[] { new DiceRoll(d4: 1, modifier: -1), "d4-1" },
                    new object[]
                    {
                        new DiceRoll(d4: 1, d6: 1, d8: 2, d10: 2, d12: 1),
                        "d12+2d10+2d8+d6+d4"
                    },
                    new object[]
                    {
                        new DiceRoll(d4: 1, d6: 1, d8: 2, d10: 2, d12: 1, modifier: 1),
                        "d12+2d10+2d8+d6+d4+1"
                    },
                    new object[]
                    {
                        new DiceRoll(d20: 1), "d20"
                    },
                    new object[]
                    {
                        new DiceRoll(d100: 1), "d100"
                    }
                };

                return cases.ToArray();
            }
        }

        [Test]
        [TestCaseSource("StringRepresentationCases")]
        public void TestToString(DiceRoll dice, string expected)
        {
            Assert.That(dice.ToString(), Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource("StringRepresentationCases")]
        public void TestParse(DiceRoll expected, string toParse)
        {
            var actual = DiceRoll.Parse(toParse);

            Assert.That(actual.D4, Is.EqualTo(expected.D4), "Number of d4s should match.");
            Assert.That(actual.D6, Is.EqualTo(expected.D6), "Number of d6s should match.");
            Assert.That(actual.D8, Is.EqualTo(expected.D8), "Number of d8s should match.");
            Assert.That(actual.D10, Is.EqualTo(expected.D10), "Number of d10s should match.");
            Assert.That(actual.D12, Is.EqualTo(expected.D12), "Number of d12s should match.");
            Assert.That(actual.D20, Is.EqualTo(expected.D20), "Number of d20s should match.");
            Assert.That(actual.D100, Is.EqualTo(expected.D100), "Number of d100s should match.");
            Assert.That(actual.Modifier, Is.EqualTo(expected.Modifier), "Modifiers should match.");
        }

        [Test]
        public void TestEquals()
        {
            var rand = new Random();

            var a = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            var b = new DiceRoll(
                d4: a.D4,
                d6: a.D6,
                d8: a.D8,
                d10: a.D10,
                d12: a.D12,
                modifier: a.Modifier);

            var c = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            // Strongly typed version
            Assert.That(a.Equals(a), Is.True);

            Assert.That(a.Equals(b), Is.True);
            Assert.That(b.Equals(a), Is.True);

            Assert.That(a.Equals(c), Is.False);
            Assert.That(c.Equals(a), Is.False);

            Assert.That(a.Equals(null), Is.False);

            // Object version
            Assert.That(((object)a).Equals(a), Is.True);

            Assert.That(((object)a).Equals(b), Is.True);
            Assert.That(((object)b).Equals(a), Is.True);

            Assert.That(((object)a).Equals(c), Is.False);
            Assert.That(((object)c).Equals(a), Is.False);

            Assert.That(((object)a).Equals(null), Is.False);
        }

        [Test]
        public void TestOperatorEquals()
        {
            var rand = new Random();

            var a = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            var b = new DiceRoll(
                d4: a.D4,
                d6: a.D6,
                d8: a.D8,
                d10: a.D10,
                d12: a.D12,
                modifier: a.Modifier);

            var c = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            Assert.That(a == a, Is.True);

            Assert.That(a == b, Is.True);
            Assert.That(b == a, Is.True);

            Assert.That(a == c, Is.False);
            Assert.That(c == a, Is.False);

            Assert.That(a == null, Is.False);
            Assert.That(null == a, Is.False);
        }

        [Test]
        public void TestOperatorNotEquals()
        {
            var rand = new Random();

            var a = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            var b = new DiceRoll(
                d4: a.D4,
                d6: a.D6,
                d8: a.D8,
                d10: a.D10,
                d12: a.D12,
                modifier: a.Modifier);

            var c = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            Assert.That(a != a, Is.False);

            Assert.That(a != b, Is.False);
            Assert.That(b != a, Is.False);

            Assert.That(a != c, Is.True);
            Assert.That(c != a, Is.True);

            Assert.That(a != null, Is.True);
            Assert.That(null != a, Is.True);
        }

        [Test]
        public void TestGetHashCode()
        {
            var rand = new Random();

            var a = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            var b = new DiceRoll(
                d4: a.D4,
                d6: a.D6,
                d8: a.D8,
                d10: a.D10,
                d12: a.D12,
                modifier: a.Modifier);

            var c = new DiceRoll(
                d4: (uint)rand.Next(),
                d6: (uint)rand.Next(),
                d8: (uint)rand.Next(),
                d10: (uint)rand.Next(),
                d12: (uint)rand.Next(),
                modifier: rand.Next());

            Assert.That(a.GetHashCode(), Is.EqualTo(a.GetHashCode()));

            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a.GetHashCode(), Is.Not.EqualTo(c.GetHashCode()));
        }

        [Test]
        public void TestMaximumRoll()
        {
            var actual = new DiceRoll(
                d4: 2,
                d6: 4,
                d8: 1,
                d10: 2,
                d12: 1,
                modifier: 5);

            Assert.That(actual.MaximumRoll, Is.EqualTo(77), "MaximumRoll should be the sum of (size * number of dice of that size) plus modifier.");
        }
    }
}
