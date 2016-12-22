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

        //ucenik
        public string imep;
        public string prezimep;
        public int klasp;
        public int brojp;
        public string emailp;
        public int profp;
        public string telp;

        //kniga
        public string naslovp;
        public string pisatelp;

        //izdadeni
        public int uceikIdp;
        public int knigaIdp;
        public string pecatBrp;
        public string datump;
        public int izvestenp;


        //konekcija
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


        //ucenik metodi
        public bool VnesNaPodUcenik(string ime, string prezime, int klas, int broj, string email, int prof, string tel)
        {
            imep = ime;
            prezimep = prezime;
            klasp = klas;
            brojp = broj;
            emailp = email;
            profp = prof;
            telp = tel;

            if (konektirano)
            {
                return true;
               
            }
            else
            {
                return false;
            }
        }
        public DataTable KveriUcenik(string kveri)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ime", typeof(string));
            dt.Columns.Add("prezime", typeof(string));
            dt.Columns.Add("klas", typeof(int));
            dt.Columns.Add("broj", typeof(int));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("prof", typeof(int));
            dt.Columns.Add("tel", typeof(string));


            DataRow dr = dt.NewRow();

            dr["ime"] = imep;
            dr["prezime"] = prezimep;
            dr["klas"] = klasp;
            dr["broj"] = brojp;
            dr["email"] = emailp;
            dr["prof"] = profp;
            dr["tel"] = telp; 
            dt.Rows.Add(dr);

            return dt;
        }

        //kniga metodi
        public bool VnesNaPodKniga(string naslov, string pisatel)
        {
            naslovp = naslov;
            pisatelp = pisatel;

            if (konektirano)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public DataTable KveriKniga(string kveri)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("naslov", typeof(string));
            dt.Columns.Add("pisatel", typeof(string));

            DataRow dr = dt.NewRow();

            dr["naslov"] = naslovp;
            dr["pisatel"] = pisatelp;

            dt.Rows.Add(dr);

            return dt;
        }

        //izdadeni metodi
        public bool VnesNaPodIzdadeni(int ucenikId, int knigaId, string pecatBr, string datum, int izvesten)
        {
            uceikIdp = ucenikId;
            knigaIdp = knigaId;
            pecatBrp = pecatBr;
            datump = datum;
            izvestenp = izvesten;

            if (konektirano)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public DataTable KveriIzdeni(string kveri)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ucenikId", typeof(int));
            dt.Columns.Add("knigaId", typeof(int));
            dt.Columns.Add("pecatBr", typeof(string));
            dt.Columns.Add("datum", typeof(string));
            dt.Columns.Add("izvesten", typeof(int));

            DataRow dr = dt.NewRow();

            dr["ucenikId"] = uceikIdp;
            dr["knigaId"] = knigaIdp;
            dr["pecatBr"] = pecatBrp;
            dr["datum"] = datump;
            dr["izvesten"] = izvestenp;

            dt.Rows.Add(dr);

            return dt;
        }
    }
}
