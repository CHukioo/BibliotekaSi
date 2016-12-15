using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MySql.Server;
using System.Diagnostics;


namespace BibliotekaSi
{
   [TestFixture]
    [TestClass]
    class UnitTest
    {
        private static readonly string _testDatabaseName = "testserver";

        [TestCase]
        public void SoberiTest()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(31, test.Soberi(20, 11));
        }

        [TestCase]
        public void VnesUcenikProverkaTest1()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(false, test.VnesUcenikProverka("Ime", "Prezime", "", "", "ime@prezime", "0", ""));
        }

        [TestCase]
        public void VnesUcenikProverkaTest2()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(true, test.VnesUcenikProverka("Ime", "Prezime", "99", "99", "ime@prezime", "0", "075123456"));
        }

        [TestCase]
        public void IzdadiProverkaTest1()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(false, test.IzdadiProverka("", "", "", ""));
        }

        [TestCase]
        public void IzdadiProverkaTest2()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(true, test.IzdadiProverka("5", "5", "2016", "5"));
        }

        [TestCase]
        public void IzdadiProverkaTest3()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(false, test.IzdadiProverka("5", "5", "2016", ""));
        }

        [TestCase]
        public void VratiProverkaTest1()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(true, test.VratiProverka("123456"));
        }

        [TestCase]
        public void VratiProverkaTest2()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(false, test.VratiProverka(""));
        }

        [TestCase]
        public void VratiProverkaTest3()
        {
            PrvTest test = new PrvTest();
            NUnit.Framework.Assert.AreEqual(false, test.VratiProverka(""));
        }


        [TestCase]
        public void ExampleTest()
        {
            //Setting up and starting the server
            //This can also be done in a AssemblyInitialize method to speed up tests
            MySqlServer dbServer = MySqlServer.Instance;
            dbServer.StartServer();

            //Create a database and select it
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(), string.Format("CREATE DATABASE {0};USE {0};", _testDatabaseName));

            //Create a table
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "CREATE TABLE testTable (`id` INT NOT NULL, `value` CHAR(150) NULL,  PRIMARY KEY (`id`)) ENGINE = MEMORY;");

            //Insert data (large chunks of data can of course be loaded from a file)
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO testTable (`id`,`value`) VALUES (1, 'some value')");
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO testTable (`id`, `value`) VALUES (2, 'test value')");

            //Load data
            using (MySqlDataReader reader = MySqlHelper.ExecuteReader(dbServer.GetConnectionString(_testDatabaseName), "select * from testTable WHERE id = 2"))
            {
                reader.Read();

                NUnit.Framework.Assert.AreEqual("test value", reader.GetString("value"), "Inserted and read string should match");
            }

            //Shutdown server
            dbServer.ShutDown();
        }

       


    }
}
