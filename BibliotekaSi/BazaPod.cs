using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using NLog;

namespace BibliotekaSi
{
    class BazaPod : IBazaPod
    {
        private static Logger log = NLog.LogManager.GetCurrentClassLogger();


        // do ovaj metod treba da se obrajca site vnesoj i brisenja na podatoci (nonquery), momentalno se
        // se obraka samo vnesiUcenikBtn od Form1
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

        // do ovaj metod treba da se obrajca query-ina, momentalno se
        // se obraka samo selectUcenik od Form1
        public DataSet Kveri(string sqlKomanda)
        {
            DataSet ds = new DataSet();
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmd;
                BindingSource bs = new BindingSource();

                String query = sqlKomanda;
                cmd = new MySqlCommand(query, conn);

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception err)
            {
                log.Error("Greska so baza (selektiraj ucenik) " + err.Message);
                return ds;
            }
        }

    }
}
