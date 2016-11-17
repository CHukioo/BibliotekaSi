using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BibliotekaSi
{
    [TestFixture]
    class UnitTests
    {
        [TestCase]
        public void Soberi()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(35, test.Soberi(20, 11));
        }
    }
}
