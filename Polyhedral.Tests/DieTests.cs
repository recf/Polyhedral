using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Polyhedral.Tests
{
    [TestFixture]
    public class DieTests
    {
        [Test]
        [TestCase(6U, new uint[] {1,2,3,4,5,6})]
        public void TestResultSpace(uint faces, uint[] resultSpace)
        {
            var die = new Die(faces);

            Assert.That(die.Faces, Is.EqualTo(faces));

            Assert.That(die.GetResultSpace(), Is.EquivalentTo(resultSpace));
        }
    }
}
