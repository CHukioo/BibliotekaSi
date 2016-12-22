using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaSi
{
    class BazaPodTest
    {
        //ovde e to kao demek so treba da bidi mokiranje

        public bool konektirano = false;

        public void KonekcijaOpen(string conString)
        {
            if (conString == "server=localhost;Database=biblioteka_si;uid=root;pwd=root;")
            {
                konektirano = true;
            }
            else
            {
                konektirano = false;
            }
        }
        public void KonekcijaClose()
        {
            konektirano = false;
            
        }


        
        public bool VnesNaPod(string sqlKomanda)
        {
            if (konektirano)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable ebago()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ime", typeof(string));
            dt.Columns.Add("godini", typeof(int));

            DataRow dr = dt.NewRow();
            dr["ime"] = "Marko"; 
            dr["godini"] = 23; 
            dt.Rows.Add(dr);

            return dt;
        }

    }
}
