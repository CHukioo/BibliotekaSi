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
        private static readonly string _testDatabaseName = "biblioteka_si";

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
        public void ConnectinTest()
        {
            //Setting up and starting the server
            //This can also be done in a AssemblyInitialize method to speed up tests
            MySqlServer dbServer = MySqlServer.Instance;
            dbServer.StartServer();

            //Create a database and select it
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(), string.Format("CREATE DATABASE {0};USE {0};", _testDatabaseName));

            //Create a table
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "CREATE TABLE `izdadeni` (`kniga_id` int(11) NOT NULL,`ucenik_id` int(11) NOT NULL,`datum` date NOT NULL,`pecat_br` varchar(255) NOT NULL,`izvesten` int(1) NOT NULL,PRIMARY KEY (`pecat_br`)) ENGINE=MEMORY;");

            //Insert data (large chunks of data can of course be loaded from a file)
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO `izdadeni` VALUES ('2', '14', '2016-10-01', '123369', '0');");
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO `izdadeni` VALUES ('2', '10', '2016-12-15', '125', '0');");

            //Load data
            using (MySqlDataReader reader = MySqlHelper.ExecuteReader(dbServer.GetConnectionString(_testDatabaseName), "select * from izdadeni WHERE pecat_br = '125'"))
            {
                reader.Read();

                NUnit.Framework.Assert.AreEqual("12", reader.GetString("ucenik_id"), "Inserted and read string should match");
            }

            //Shutdown server
            dbServer.ShutDown();
        }

       


    }
}
