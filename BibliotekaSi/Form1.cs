﻿using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using NLog;

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

            if (checkBox1.Checked)
            {
                prof = "1";
            }

            PrvTest klasa = new PrvTest();
            if (klasa.VnesUcenikProverka(ime, prezime, klas, broj, email, prof)) { 

                try {
               
                    String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                    MySqlConnection conn = new MySqlConnection(konekcija);

                    conn.Open();

                    String komanda = "insert into ucenik (ime, prezime, klas, broj, email, profesor) values (@ime, @prezime, @klas, @broj, @email, @profesor)";
                    MySqlCommand cmd = new MySqlCommand(komanda, conn);


                    cmd.Parameters.AddWithValue("@ime", textBox1.Text);
                    cmd.Parameters.AddWithValue("@prezime", textBox2.Text);
                    cmd.Parameters.AddWithValue("@klas", textBox3.Text);
                    cmd.Parameters.AddWithValue("@broj", textBox4.Text);
                    cmd.Parameters.AddWithValue("@email", textBox5.Text);
                    cmd.Parameters.AddWithValue("@profesor", prof);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                        MessageBox.Show("Успешен внес за: "+textBox1.Text+" "+textBox2.Text);
                    selectUcenik();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                catch (MySqlException err)
                {
                    MessageBox.Show(err.Message);
                    log.Error("Nema konekcija do baza"+err.Message);
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

            String query = "select ime, prezime, klas, broj, email from ucenik where profesor=0";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }
        public void selectKniga()
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select naslov, pisatel from kniga";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView2.DataSource = bs;
        }

        //searchbar za ucenik/kniga
        private void prebaruvanjeUcenikBox(object sender, KeyPressEventArgs e)
        {
            String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
            MySqlConnection conn = new MySqlConnection(konekcija);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            String query = "select ime, prezime, klas, broj, email from ucenik where prezime like '%" + textBox6.Text + "%'";
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

            String query = "select naslov, pisatel from kniga where naslov like '%" + textBox8.Text + "%'";
            cmd = new MySqlCommand(query, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];
            dataGridView2.DataSource = bs;
        }

        //na mousclikc vo tabela se selektira red
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox11.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
        }

        

    }
}
