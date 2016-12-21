using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using NLog;

namespace BibliotekaSi
{
    class BazaPod
    {
        private static Logger log = NLog.LogManager.GetCurrentClassLogger();

        public string VnesPodatoci(string sqlKomanda)
        {
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = sqlKomanda;
                MySqlCommand cmd = new MySqlCommand(komanda, conn);



                cmd.ExecuteNonQuery();

                conn.Close();
                return "ok";
            }
            catch (MySqlException err)
            {
                return err.Message;
            }
        }
    }
}
