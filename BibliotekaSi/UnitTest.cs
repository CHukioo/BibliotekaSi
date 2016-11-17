using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    [TestFixture]
    class UnitTest
    {
        [TestCase]
        public void Add()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(31, test.Soberi(20, 11));
        }

        [TestCase]
        public void ajdavidam()
        {
            Form1 kur = new Form1();
        }
    }
}
