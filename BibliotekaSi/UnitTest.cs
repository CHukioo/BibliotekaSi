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
        public void BazaKonekcijaVnesIznesPodatociKniga()
        {
            //Setting up and starting the server
            //This can also be done in a AssemblyInitialize method to speed up tests
            MySqlServer dbServer = MySqlServer.Instance;
            dbServer.StartServer();

            //Create a database and select it
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(), string.Format("CREATE DATABASE {0};USE {0};", _testDatabaseName));

            //Create a table
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "CREATE TABLE `kniga` ( `kniga_id` int(11) NOT NULL AUTO_INCREMENT, `naslov` varchar(255) NOT NULL, `pisatel` varchar(255) NOT NULL, PRIMARY KEY(`kniga_id`) ENGINE =MEMORY; ");

            //Insert data (large chunks of data can of course be loaded from a file)
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO `kniga` VALUES ('2', 'Zoki Poki', 'Olivera Nikolova');");
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO `kniga` VALUES ('3', 'Vojna i mir', 'Nz Pisatel');");

            //Load data
            using (MySqlDataReader reader = MySqlHelper.ExecuteReader(dbServer.GetConnectionString(_testDatabaseName), "select * from kniga WHERE naslov = 'Zoki Poki'"))
            {
                reader.Read();

                NUnit.Framework.Assert.AreEqual("2", reader.GetString("kniga_id"), "Inserted and read string should match");
            }

            //Shutdown server
            dbServer.ShutDown();
        }

        [TestCase]
        public void BazaKonekcijaVnesIznesPodatociIzdadeni()
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

                NUnit.Framework.Assert.AreEqual("10", reader.GetString("ucenik_id"), "Inserted and read string should match");
            }

            //Shutdown server
            dbServer.ShutDown();
        }

        [TestCase]
        public void BazaKonekcijaVnesIznesPodatociUcenik()
        {
            //Setting up and starting the server
            //This can also be done in a AssemblyInitialize method to speed up tests
            MySqlServer dbServer = MySqlServer.Instance;
            dbServer.StartServer();

            //Create a database and select it
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(), string.Format("CREATE DATABASE {0};USE {0};", _testDatabaseName));

            //Create a table
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "CREATE TABLE `ucenik` ( `ucenik_id` int(11) NOT NULL AUTO_INCREMENT, `ime` varchar(255) NOT NULL, `prezime` varchar(255) NOT NULL, `klas` int(11) DEFAULT NULL, `broj` int(11) DEFAULT NULL, `email` varchar(255) NOT NULL, `profesor` int(11) NOT NULL, `telefon` varchar(9) NOT NULL, PRIMARY KEY(`ucenik_id`)) ENGINE=MEMORY; ");

            //Insert data (large chunks of data can of course be loaded from a file)
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO `ucenik` VALUES ('14', 'Ilija', 'Jolevski', '0', '0', 'ilija@gmail.com', '1', '123456789');");
            MySqlHelper.ExecuteNonQuery(dbServer.GetConnectionString(_testDatabaseName), "INSERT INTO `ucenik` VALUES ('10', 'Andrej', 'Gagaleski', '41', '23', 'markocurlinoski.uie@gmail.com', '0', '123456789');");

            //Load data
            using (MySqlDataReader reader = MySqlHelper.ExecuteReader(dbServer.GetConnectionString(_testDatabaseName), "select * from ucenik WHERE email = 'markocurlinoski.uie@gmail.com'"))
            {
                reader.Read();

                NUnit.Framework.Assert.AreEqual("10", reader.GetString("ucenik_id"), "Inserted and read string should match");
            }

            //Shutdown server
            dbServer.ShutDown();
        }

        




    }
}
