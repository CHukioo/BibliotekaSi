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
    public class KnigakMockTests
    {
        public KnigakMockTests()
        {
            // create some mock products to play with
            IList<Kniga> knigi = new List<Kniga>
                {
                    new Kniga { KnigaId = 1, Naslov = "Test1", Pisatel = "Test1" },
                    new Kniga { KnigaId = 2, Naslov = "Test2", Pisatel = "Test2" },
                    new Kniga { KnigaId = 3, Naslov = "Test3", Pisatel = "Test3" }
                };

            // Mock the Products Repository using Moq
            Mock<IKnigaRepository> mockKnigaRepository = new Mock<IKnigaRepository>();

            // Return all the products
            mockKnigaRepository.Setup(mr => mr.Site()).Returns(knigi);

            // return a product by Id
            mockKnigaRepository.Setup(mr => mr.SelektPoId(It.IsAny<int>())).Returns((int i) => knigi.Where(x => x.KnigaId == i).Single());

            // Allows us to test saving a product
            mockKnigaRepository.Setup(mr => mr.VnesiKniga(It.IsAny<Kniga>())).Returns(
                (Kniga target) =>
                {

                    if (target.KnigaId.Equals(default(int)))
                    {

                        target.KnigaId = knigi.Count() + 1;
                        knigi.Add(target);
                    }
                    else
                    {
                        var original = knigi.Where(q => q.KnigaId == target.KnigaId).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.KnigaId = target.KnigaId;
                        original.Naslov = target.Naslov;
                        original.Pisatel = target.Pisatel;

                    }
                    return true;
                });

            // Complete the setup of our Mock Product Repository
            this.MockKnigaRepository = mockKnigaRepository.Object;
        }
        public NUnit.Framework.TestContext TestContext { get; set; }
        public readonly IKnigaRepository MockKnigaRepository;

        [TestCase]
        public void SelectPoIdKnigaMockTest()
        {
            // Try finding a product by id
            Kniga testKniga = this.MockKnigaRepository.SelektPoId(2);

            NUnit.Framework.Assert.IsNotNull(testKniga); // Test if null
            NUnit.Framework.Assert.IsInstanceOf<Kniga>(testKniga);
            //   Type(testProduct, typeof(Product)); // Test type
            NUnit.Framework.Assert.AreEqual("Test2", testKniga.Naslov); // Verify it is the right product
        }

        [TestCase]
        public void SelectAllKnigaMockTest()
        {
            // Try finding all products
            IList<Kniga> testKniga = this.MockKnigaRepository.Site();

            NUnit.Framework.Assert.IsNotNull(testKniga); // Test if null
            NUnit.Framework.Assert.AreEqual(4, testKniga.Count); // Verify the correct Number
        }

        [TestCase]
        public void InsertKnigaMockTest()
        {
            // Create a new product, not I do not supply an id
            Kniga novaKniga = new Kniga
            { Naslov = "Test4", Pisatel = "Test4" };

            int knigaCount = this.MockKnigaRepository.Site().Count;
            NUnit.Framework.Assert.AreEqual(3, knigaCount); // Verify the expected Number pre-insert

            // try saving our new product
            this.MockKnigaRepository.VnesiKniga(novaKniga);

            // demand a recount
            knigaCount = this.MockKnigaRepository.Site().Count;
            NUnit.Framework.Assert.AreEqual(4, knigaCount); // Verify the expected Number post-insert
        }
    }
}