using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    class PrvTest
    {
        Logger log = NLog.LogManager.GetCurrentClassLogger();


        public int Soberi (int a, int b)
        {
            return a + b;
        }

        public bool VnesUcenikProverka(string ime, string prezime, string klas, string br, string email, string profesor, string telefon)
        {
            if (profesor == "0") { 
                if (ime != "" && prezime!="" && klas != "" && br != "" && email != "" && profesor != "" && telefon !="")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (ime != "" && prezime != "" && email != "" && profesor != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IzdadiProverka(string ucenik, string kniga, string datum, string pecat)
        {

            if (ucenik != "" && kniga != "" && datum != "" && pecat != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VratiProverka(string pecatBr)
        {

            if (pecatBr != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaliUcenikImaKniga(string ucenikId)
        {
            bool vrati = true;
           
                DataTable tabela = new DataTable();
                try
                {
                    String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                    MySqlConnection conn = new MySqlConnection(konekcija);

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd;

                    String query = "SELECT ucenik.profesor, izdadeni.pecat_br FROM izdadeni Left Join ucenik ON izdadeni.ucenik_id = ucenik.ucenik_id WHERE ucenik.ucenik_id="+ucenikId+";";
                    cmd = new MySqlCommand(query, conn);

                    adapter.SelectCommand = cmd;
                    adapter.Fill(tabela);                                      
                }
                catch (Exception err)
                {
                    log.Error("Greska so proverka na ucenik dali ima zemeno kniga "+err.Message);
                }
                foreach (DataRow row in tabela.Rows)
                {
                    if (row["profesor"].ToString() == "1")
                    {
                        vrati = true;
                        break;
                    }
                    if (row["profesor"].ToString() == "0" && tabela.Rows.Count == 1)
                    {
                        vrati = false;
                        break;
                    }
                }

            return vrati;
            }
        }
}
