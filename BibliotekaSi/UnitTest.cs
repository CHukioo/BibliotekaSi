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
        public void SoberiTest()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(31, test.Soberi(20, 11));
        }

        [TestCase]
        public void VnesUcenikProverkaTest1()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(false, test.VnesUcenikProverka("Ime", "Prezime", "", "", "ime@prezime", "0"));
        }

        [TestCase]
        public void VnesUcenikProverkaTest2()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(true, test.VnesUcenikProverka("Ime", "Prezime", "99", "99", "ime@prezime", "0"));
        }

        [TestCase]
        public void IzdadiProverkaTest1()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(false, test.IzdadiProverka("", "", "", ""));
        }

        [TestCase]
        public void IzdadiProverkaTest2()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(true, test.IzdadiProverka("5", "5", "2016", "5"));
        }

        [TestCase]
        public void IzdadiProverkaTest3()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(false, test.IzdadiProverka("5", "5", "2016", ""));
        }

        [TestCase]
        public void VratiProverkaTest1()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(true, test.VratiProverka("123456"));
        }

        [TestCase]
        public void VratiProverkaTest2()
        {
            PrvTest test = new PrvTest();
            Assert.AreEqual(false, test.VratiProverka(""));
        }
    }
}
