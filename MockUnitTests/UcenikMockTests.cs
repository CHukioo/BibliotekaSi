using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BibliotekaSi;

namespace MockUnitTests
{
    [TestClass]
    public class UcenikMockTests
    {
        public UcenikMockTests()
        {
            // create some mock products to play with
            IList<Ucenik> ucenici = new List<Ucenik>
                {
                    new Ucenik { UcenikId = 1, Ime = "Test1", Prezime = "Test1", Broj = 11, Klas = 11, Email = "test1@gmail.com", Profesor = 0, Telefon = "75111111" },
                    new Ucenik { UcenikId = 2, Ime = "Test2", Prezime = "Test2", Broj = 22, Klas = 22, Email = "test2@gmail.com", Profesor = 0, Telefon = "75222222" },
                    new Ucenik { UcenikId = 3, Ime = "Test3", Prezime = "Test3", Broj = 33, Klas = 33, Email = "test3@gmail.com", Profesor = 0, Telefon = "75333333" }
                };

            // Mock the Products Repository using Moq
            Mock<IUcenikRepository> mockUcenikRepository = new Mock<IUcenikRepository>();

            // Return all the products
            mockUcenikRepository.Setup(mr => mr.Site()).Returns(ucenici);

            // return a product by Id
            mockUcenikRepository.Setup(mr => mr.SelektPoId(It.IsAny<int>())).Returns((int i) => ucenici.Where(x => x.UcenikId == i).Single());

            // delete product by id
            mockUcenikRepository.Setup(mr => mr.DeletPoId(It.IsAny<int>())).Callback((int uid) => ucenici.Where(x => x.UcenikId == uid).Single());

            // Allows us to test saving a product
            mockUcenikRepository.Setup(mr => mr.VnesiUcenik(It.IsAny<Ucenik>())).Returns(
                (Ucenik target) =>
                {

                    if (target.UcenikId.Equals(default(int)))
                    {

                        target.UcenikId = ucenici.Count() + 1;
                        ucenici.Add(target);
                    }
                    else
                    {
                        var original = ucenici.Where(q => q.UcenikId == target.UcenikId).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.UcenikId = target.UcenikId;
                        original.Ime = target.Ime;
                        original.Prezime = target.Prezime;
                        original.Klas = target.Klas;
                        original.Broj = target.Broj;
                        original.Email = target.Email;
                        original.Profesor = target.Profesor;
                        original.Telefon = target.Telefon;

                    }
                    return true;
                });

            // Complete the setup of our Mock Product Repository
            this.MockUcenikRepository = mockUcenikRepository.Object;
        }
        public NUnit.Framework.TestContext TestContext { get; set; }
        public readonly IUcenikRepository MockUcenikRepository;

        [TestCase]
        public void SelectPoIdUcenikMockTest()
        {
            // Try finding a product by id
            Ucenik testUcenik = this.MockUcenikRepository.SelektPoId(2);

            NUnit.Framework.Assert.IsNotNull(testUcenik); // Test if null
            NUnit.Framework.Assert.IsInstanceOf<Ucenik>(testUcenik);
            //   Type(testProduct, typeof(Product)); // Test type
            NUnit.Framework.Assert.AreEqual("Test2", testUcenik.Ime); // Verify it is the right product
        }

        [TestCase]
        public void SelectAllUcenikMockTest()
        {
            // Try finding all products
            IList<Ucenik> testUcenik = this.MockUcenikRepository.Site();

            NUnit.Framework.Assert.IsNotNull(testUcenik); // Test if null
            NUnit.Framework.Assert.AreEqual(4, testUcenik.Count); // Verify the correct Number
        }

        [TestCase]
        public void InsertUcenikMockTest()
        {
            // Create a new product, not I do not supply an id
            Ucenik novUcenik = new Ucenik
            { Ime = "TestUcenik", Prezime = "TestUcenik", Klas = 11, Broj = 11, Email = "TestUcenik", Profesor = 0, Telefon = "75999999" };

            int ucenikCount = this.MockUcenikRepository.Site().Count;
            NUnit.Framework.Assert.AreEqual(3, ucenikCount); // Verify the expected Number pre-insert

            // try saving our new product
            this.MockUcenikRepository.VnesiUcenik(novUcenik);

            // demand a recount
            ucenikCount = this.MockUcenikRepository.Site().Count;
            NUnit.Framework.Assert.AreEqual(4, ucenikCount); // Verify the expected Number post-insert
        }

        [TestCase]
        public void DeleteKnigaMockTest()
        {
            this.MockUcenikRepository.DeletPoId(1);
            int ucenikCount = this.MockUcenikRepository.Site().Count;
            NUnit.Framework.Assert.AreEqual(3, ucenikCount);

        }
    }
}
