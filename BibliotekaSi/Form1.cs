using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using NLog;
using System.Net.Mail;
using System.Net;

namespace BibliotekaSi
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            selectUcenik();
            selectKniga();
            selectIzdadeni();

            datum();
            Logger log = LogManager.GetCurrentClassLogger();
            log.Info("Programata startuvana");

        }

        //vnes na podatoci vo baza(ucenik/kniga)
        private void vnesiUcenikBtn(object sender, EventArgs e)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            //definiram vrednosti sto ce vleza vo metod VnesUcenikProverka vo PrvTest
            String prof = "0";
            String ime = textBox1.Text;
            String prezime = textBox2.Text;
            String klas = textBox3.Text;
            String broj = textBox4.Text;
            String email = textBox5.Text;
            String telefon = textBox19.Text;

            if (checkBox1.Checked)
            {
                prof = "1";
            }

            PrvTest klasa = new PrvTest();
            if (klasa.VnesUcenikProverka(ime, prezime, klas, broj, email, prof, telefon))
            {

                try
                {

                    String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                    MySqlConnection conn = new MySqlConnection(konekcija);

                    conn.Open();

                    String komanda = "insert into ucenik (ime, prezime, klas, broj, email, profesor, telefon) values (@ime, @prezime, @klas, @broj, @email, @profesor, @telefon)";
                    MySqlCommand cmd = new MySqlCommand(komanda, conn);


                    cmd.Parameters.AddWithValue("@ime", textBox1.Text);
                    cmd.Parameters.AddWithValue("@prezime", textBox2.Text);
                    cmd.Parameters.AddWithValue("@klas", textBox3.Text);
                    cmd.Parameters.AddWithValue("@broj", textBox4.Text);
                    cmd.Parameters.AddWithValue("@email", textBox5.Text);
                    cmd.Parameters.AddWithValue("@profesor", prof);
                    cmd.Parameters.AddWithValue("@telefon", telefon);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    MessageBox.Show("Успешен внес за: " + textBox1.Text + " " + textBox2.Text);
                    selectUcenik();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox16.Text = "";
                }
                catch (MySqlException err)
                {
                    MessageBox.Show(err.Message);
                    log.Error("Nema konekcija do baza" + err.Message);
                }
            }
            else
            {
                MessageBox.Show("Неправилно внесени податоци!");
                log.Info("Nepravilno vneseni podatoci za Ucenik/Profesor");
            }
        }
        private void vnesiKnigaBtn(object sender, EventArgs e)
        {
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "insert into kniga (naslov, pisatel) values (@naslov, @pisatel)";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);


                cmd.Parameters.AddWithValue("@naslov", textBox9.Text);
                cmd.Parameters.AddWithValue("@pisatel", textBox10.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Успешен внес за: " + textBox9.Text);
                selectKniga();

                textBox9.Text = "";
                textBox10.Text = "";
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message);
            }
        }

        //brisenje na podatoci od baza (ucenik/kniga)
        private void brisiUcenikBtn(object sender, EventArgs e)
        {
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "delete from ucenik where email='" + textBox7.Text + "'";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Успешно бришење!");
                selectUcenik();
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }
        private void brisiKnigaBtn(object sender, EventArgs e)
        {
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "delete from kniga where naslov='" + textBox11.Text + "'";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Успешно бришење!");
                selectKniga();
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        // metod koj go polni data grido kaj ucenik/kniga
        public void selectUcenik()
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select ucenik_id, ime, prezime, klas, broj, email from ucenik where profesor=0";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
            dataGridView3.DataSource = bs;

        }
        public void selectKniga()
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select kniga_id, naslov, pisatel from kniga";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView2.DataSource = bs;
            dataGridView4.DataSource = bs;
        }
        public void selectIzdadeni()
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "SELECT izdadeni.pecat_br, kniga.naslov, ucenik.ime, ucenik.prezime, ucenik.ucenik_id FROM izdadeni Left Join kniga ON izdadeni.kniga_id = kniga.kniga_id Left Join ucenik ON ucenik.ucenik_id = izdadeni.ucenik_id; ";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView5.DataSource = bs;
        }

        //searchbar za vnes na ucenik/kniga 
        private void prebaruvanjeUcenikBox(object sender, KeyPressEventArgs e)
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select ucenik_id, ime, prezime, klas, broj, email from ucenik where prezime like '%" + textBox6.Text + "%'";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }
        private void prebaruvanjeKnigaBox(object sender, KeyPressEventArgs e)
        {

            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select kniga_id, naslov, pisatel from kniga where naslov like '%" + textBox8.Text + "%'";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView2.DataSource = bs;
        }

        //searchbar za izdaj na ucenik/kniga 
        private void prebaruvanjeUcenikBoxIzdaj(object sender, KeyPressEventArgs e)
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select ucenik_id, ime, prezime, klas, broj, email from ucenik where prezime like '%" + textBox12.Text + "%'";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView3.DataSource = bs;
        }
        private void prebaruvanjeKnigaBoxIzdaj(object sender, KeyPressEventArgs e)
        {

            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select kniga_id, naslov, pisatel from kniga where naslov like '%" + textBox13.Text + "%'";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView4.DataSource = bs;
        }

        //na mousclikc vo tabela se selektira red (vnesi)
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }
        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox11.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
        }

        //na mousclikc vo tabela se selektira red (izdaj)
        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox14.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
        }
        private void dataGridView4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox15.Text = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
        }

        //na mousclikc vo tabela se selektira red (izdaj)
        private void dataGridView5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox18.Text = dataGridView5.SelectedRows[0].Cells[0].Value.ToString();
        }

        //denesen datum pogoden za sql
        private void datum()
        {
            DateTime data = DateTime.Today;
            DateTime sqlDate = data.Date;
            textBox16.Text = sqlDate.ToString("yyyy-MM-dd");
        }

        //izdavanje na kniga
        private void izdadiKnigaBtn(object sender, EventArgs e)
        {
            Logger log = LogManager.GetCurrentClassLogger();

            //definiram vrednosti sto ce vleza vo metod IzdadiProverka vo PrvTest
            String ucenik = textBox14.Text;
            String kniga = textBox15.Text;
            String datum = textBox16.Text;
            String pecat = textBox17.Text;

            PrvTest klasa = new PrvTest();
            if (klasa.IzdadiProverka(ucenik, kniga, datum, pecat))
            {

                try
                {

                    String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                    MySqlConnection conn = new MySqlConnection(konekcija);

                    conn.Open();

                    String komanda = "insert into izdadeni (kniga_id, ucenik_id, datum, pecat_br, izvesten) values (@kniga_id, @ucenik_id, @datum, @pecat_br, 0)";
                    MySqlCommand cmd = new MySqlCommand(komanda, conn);


                    cmd.Parameters.AddWithValue("@ucenik_id", textBox14.Text);
                    cmd.Parameters.AddWithValue("@kniga_id", textBox15.Text);
                    cmd.Parameters.AddWithValue("@datum", textBox16.Text);
                    cmd.Parameters.AddWithValue("@pecat_br", textBox17.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    MessageBox.Show("Успешен издадена книга!");
                    selectUcenik();

                    textBox14.Text = "";
                    textBox15.Text = "";
                    textBox17.Text = "";
                }
                catch (MySqlException err)
                {
                    MessageBox.Show(err.Message);
                    log.Error("Nema konekcija do baza " + err.Message);
                }
            }
            else
            {
                MessageBox.Show("Неправилно внесени податоци!");
                log.Info("Nepravilno vneseni podatoci za Ucenik/Profesor");
            }
            selectIzdadeni();
        }


        //kopce za vrakanje na kniga
        private void knigaVratiBtn(object sender, EventArgs e)
        {
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "delete from izdadeni where pecat_br='" + textBox18.Text + "'";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Успешно враќање!");
                selectIzdadeni();
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        //izvestuvanja email i sms
        public void Email(string ucenik, string kniga, string pecat, string email, int progresStep)
        {


            try
            {
                SmtpClient klient = new SmtpClient("smtp-pulse.com", 2525);
                klient.EnableSsl = false;
                klient.Timeout = 100000;
                klient.DeliveryMethod = SmtpDeliveryMethod.Network;
                klient.UseDefaultCredentials = false;
                klient.Credentials = new NetworkCredential("biblioteka_si@hotmail.com", "pRERWCSZstBc");

                MailMessage msg = new MailMessage();
                msg.To.Add(email.ToString());
                msg.From = new MailAddress("biblioteka_si@hotmail.com");
                msg.IsBodyHtml = true;
                msg.Subject = "Известување од Biblioteka Si";
                msg.Body = "<!DOCTYPE html> <head> <!-- If you delete this meta tag, Half Life 3 will never be released. --> <meta name='viewport' content='width=device-width' /> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /> <title>Biblioteka Si</title> <style type='text/css'> /* ------------------------------------- GLOBAL ------------------------------------- */ * {margin:0; padding:0; } * { font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; } img {max-width: 100%; width: 200px; height: auto; } .collapse {margin:0; padding:0; } body {-webkit-font-smoothing:antialiased; -webkit-text-size-adjust:none; width: 100%!important; height: 100%; } /* ------------------------------------- ELEMENTS ------------------------------------- */ a { color: #2BA6CB;} .btn {text-decoration:none; color: #FFF; background-color: #666; padding:10px 16px; font-weight:bold; margin-right:10px; text-align:center; cursor:pointer; display: inline-block; } p.callout {padding:15px; background-color:#ECF8FF; margin-bottom: 15px; } .callout a {font-weight:bold; color: #2BA6CB; } table.social {/* 	padding:15px; */ background-color: #ebebeb; } .social .soc-btn {padding: 3px 7px; font-size:12px; margin-bottom:10px; text-decoration:none; color: #FFF;font-weight:bold; display:block; text-align:center; } a.fb { background-color: #3B5998!important; } a.tw { background-color: #1daced!important; } a.gp { background-color: #DB4A39!important; } a.ms { background-color: #000!important; } .sidebar .soc-btn {display:block; width:100%; } /* ------------------------------------- HEADER ------------------------------------- */ table.head-wrap { width: 100%;} .header.container table td.logo { padding: 15px; } .header.container table td.label { padding: 15px; padding-left:0px;} /* ------------------------------------- BODY ------------------------------------- */ table.body-wrap { width: 100%;} /* ------------------------------------- FOOTER ------------------------------------- */ table.footer-wrap { width: 100%;	clear:both!important; } .footer-wrap .container td.content  p { border-top: 1px solid rgb(215,215,215); padding-top:15px;} .footer-wrap .container td.content p {font-size:10px; font-weight: bold; } /* ------------------------------------- TYPOGRAPHY ------------------------------------- */ h1,h2,h3,h4,h5,h6 {font-family: 'HelveticaNeue-Light', 'Helvetica Neue Light', 'Helvetica Neue', Helvetica, Arial, 'Lucida Grande', sans-serif; line-height: 1.1; margin-bottom:15px; color:#000; } h1 small, h2 small, h3 small, h4 small, h5 small, h6 small { font-size: 60%; color: #6f6f6f; line-height: 0; text-transform: none; } h1 { font-weight:200; font-size: 44px;} h2 { font-weight:200; font-size: 37px;} h3 { font-weight:500; font-size: 27px;} h4 { font-weight:500; font-size: 23px;} h5 { font-weight:900; font-size: 17px;} h6 { font-weight:900; font-size: 14px; text-transform: uppercase; color:#444;} .collapse { margin:0!important;} p, ul {margin-bottom: 10px; font-weight: normal; font-size:14px; line-height:1.6; } p.lead { font-size:17px; } p.last { margin-bottom:0px;} ul li {margin-left:5px; list-style-position: inside; } /* ------------------------------------- SIDEBAR ------------------------------------- */ ul.sidebar {background:#ebebeb; display:block; list-style-type: none; } ul.sidebar li { display: block; margin:0;} ul.sidebar li a {text-decoration:none; color: #666; padding:10px 16px; /* 	font-weight:bold; */ margin-right:10px; /* 	text-align:center; */ cursor:pointer; border-bottom: 1px solid #777777; border-top: 1px solid #FFFFFF; display:block; margin:0; } ul.sidebar li a.last { border-bottom-width:0px;} ul.sidebar li a h1,ul.sidebar li a h2,ul.sidebar li a h3,ul.sidebar li a h4,ul.sidebar li a h5,ul.sidebar li a h6,ul.sidebar li a p { margin-bottom:0!important;} /* --------------------------------------------------- RESPONSIVENESS Nuke it from orbit. It's the only way to be sure. ------------------------------------------------------ */ /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */ .container {display:block!important; max-width:600px!important; margin:0 auto!important; /* makes it centered */ clear:both!important; } /* This should also be a block element, so that it will fill 100% of the .container */ .content {padding:15px; max-width:600px; margin:0 auto; display:block; } /* Let's make sure tables in the content area are 100% wide */ .content table { width: 100%; } /* Odds and ends */ .column {width: 300px; float:left; } .column tr td { padding: 15px; } .column-wrap {padding:0!important; margin:0 auto; max-width:600px!important; } .column table { width:100%;} .social .column {width: 280px; min-width: 279px; float:left; } /* Be sure to place a .clear element after each set of columns, just to be safe */ .clear { display: block; clear: both; } /* ------------------------------------------- PHONE For clients that support media queries. Nothing fancy. -------------------------------------------- */ @media only screen and (max-width: 600px) {a[class='btn'] { display:block!important; margin-bottom:10px!important; background-image:none!important; margin-right:0!important;} div[class='column'] { width: auto!important; float:none!important;} table.social div[class='column'] {width:auto!important; } } </style> </head> <body bgcolor='#FFFFFF'> <!-- HEADER --> <table class='head-wrap' bgcolor='#999999'> <tr> <td></td> <td class='header container' > <div class='content'> <table bgcolor='#999999'> <tr> <td><img src='http://cisco.uklo.edu.mk/fikt.png' /></td> <td align='right'><h6 class='collapse'>Biblioteka Si</h6></td> </tr> </table> </div> </td> <td></td> </tr> </table><!-- /HEADER --> <!-- BODY --> <table class='body-wrap'> <tr> <td></td> <td class='container' bgcolor='#FFFFFF'> <div class='content'> <table> <tr> <td> <h3>Добар ден, " + ucenik + "</h3> <p class='lead'>Сакаме да ти звестме дека веќе подолго време кај себе ја држиш книгата " + kniga + " со печат број: " + pecat + " и истата од непознати причини не сте ја вратиле. Ве молиме истото да го сторите во најкус можен рок.</p> <!-- Callout Panel --> <p class='callout'> Доколку не ја вратете книга во рок од 2 недели следува ригоозна казна. </p><!-- /Callout Panel --> <!-- social & contact --> <table class='social' width='100%'> <tr> <td> <!-- column 1 --> <table align='left' class='column'> <tr> <td> </td> </tr> </table><!-- /column 1 --> <!-- column 2 --> <table align='left' class='column'> <tr> <td> <h5 class=''>Со почит</h5> <p>Библиотекар: <strong>Сузана Т. Чурлиноска</strong><br/> Емаил: <strong><a href='biblioteka_si@hotmail.com'>biblioteka_si@hotmail.com</a></strong></p> </td> </tr> </table><!-- /column 2 --> <span class='clear'></span> </td> </tr> </table><!-- /social & contact --> </td> </tr> </table> </div><!-- /content --> </td> <td></td> </tr> </table><!-- /BODY --> </body> </html>";
                // msg.Body = "<style type='text/css'> .tg  {border-collapse:collapse;border-spacing:0;} .tg td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;} .tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;} .tg .tg-baqh{text-align:center;vertical-align:top} .tg .tg-lqy6{text-align:right;vertical-align:top} img{width: 200; height: auto;} table{margin: 0 auto; width: 50%;}</style> <table class='tg' border='0'> <tr> <th class='tg-baqh'><img src='http://www.fikt.uklo.edu.mk/assets/uploads/sites/2/2015/03/fict_logo.png' height='100' width='auto' ></th> <th class='tg-lqy6'>Biblioteka Si</th> </tr> <tr> <td class='tg-baqh' colspan='2'>Добар ден, Марко Чурлиноски<br>Сакаме да ти звестме дека веќе подолго време кај себе ја држиш книгата Дон Кихот и истата од непознати причини не сте ја вратиле. Ве молиме истото да го сторите во најкус можен рок.</td> </tr> <tr> <td class='tg-baqh'></td> <td class='tg-lqy6'>Библиотекар: Сузана Чурлиноска<br>Емаил: biblioteka_si@hotmail.com</td> </tr> </table>";
                klient.Send(msg);


                //kazuvam na bazata deka se izvesteni
                progressBar1.Increment(progresStep);
                updateIzvesten(pecat);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        public void updateIzvesten(string pecat)
        {
            try
            {

                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "UPDATE izdadeni SET izvesten = 1 WHERE pecat_br = @email ";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);


                cmd.Parameters.AddWithValue("@email", pecat);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message);
                //  log.Error("Nema konekcija do baza" + err.Message);
            }
        }
        private void pratiEmailBtn(object sender, EventArgs e)
        {
            try
            {
                DataTable tabela = new DataTable();

                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmd;

                String query = "SELECT izdadeni.pecat_br, kniga.naslov, ucenik.ime, ucenik.prezime, ucenik.email FROM izdadeni Left Join kniga ON izdadeni.kniga_id = kniga.kniga_id Left Join ucenik ON ucenik.ucenik_id = izdadeni.ucenik_id WHERE izdadeni.datum < NOW() - INTERVAL 21 DAY AND izdadeni.izvesten = 0 ";
                cmd = new MySqlCommand(query, conn);

                adapter.SelectCommand = cmd;
                adapter.Fill(tabela);

                int progresStep = 0;

                if (tabela.Rows.Count != 0)
                {
                    progresStep = 100 / tabela.Rows.Count;
                }
                else
                {
                    MessageBox.Show("Нема никој за известување!");
                }

                foreach (DataRow row in tabela.Rows)
                {
                    string ucenik = row["ime"].ToString() + " " + row["prezime"].ToString();
                    string kniga = row["naslov"].ToString();
                    string pecat = row["pecat_br"].ToString();
                    string email = row["email"].ToString();


                    Email(ucenik, kniga, pecat, email, progresStep);
                }
                progressBar1.Increment(100);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        public void Sms(string kniga, string broj)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    string url = " http://smsc.vianett.no/v3/send.ashx?" +
                        "src=389" + broj + "&"+
                        "dst=389" + broj + " & "+
                        "msg=Ve+molime+da+ja+vratite+knigata+" +kniga+"&" +
                        "username=markocurlinoski.uie@gmail.com&" +
                        "password=9spbi";
                    string result = client.DownloadString(url);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }

        }
        private void pratiEmailSmsBtn(object sender, EventArgs e)
        {
            try
            {
                DataTable tabela = new DataTable();

                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand cmd;

                String query = "SELECT izdadeni.pecat_br, kniga.naslov, ucenik.ime, ucenik.prezime, ucenik.email ucenik.telefon FROM izdadeni Left Join kniga ON izdadeni.kniga_id = kniga.kniga_id Left Join ucenik ON ucenik.ucenik_id = izdadeni.ucenik_id WHERE izdadeni.datum < NOW() - INTERVAL 21 DAY AND izdadeni.izvesten = 0 ";
                cmd = new MySqlCommand(query, conn);

                adapter.SelectCommand = cmd;
                adapter.Fill(tabela);

                int progresStep = 0;

                if (tabela.Rows.Count != 0)
                {
                    progresStep = 100 / tabela.Rows.Count;
                }
                else
                {
                    MessageBox.Show("Нема никој за известување!");
                }

                foreach (DataRow row in tabela.Rows)
                {
                    string ucenik = row["ime"].ToString() + " " + row["prezime"].ToString();
                    string kniga = row["naslov"].ToString();
                    string pecat = row["pecat_br"].ToString();
                    string email = row["email"].ToString();
                    string telefon = row["telefon"].ToString();

                    Sms(kniga, telefon);
                    Email(ucenik, kniga, pecat, email, progresStep);

                }
                progressBar1.Increment(100);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }


        //menuvanje na skolska godina
        private void smeniSkolskaGodina(object sender, EventArgs e)
        {
            try
            {
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "UPDATE ucenik SET klas = klas + 10 WHERE klas > 0;";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);
                cmd.ExecuteNonQuery();

                komanda = "DELETE FROM ucenik WHERE klas > 50;";
                cmd = new MySqlCommand(komanda, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("СМЕНЕТО");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }


    }
}
