using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

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
            select();
            selectKniga();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try {
                String prof = "0";
                if (checkBox1.Checked)
                {
                    prof = "1";
                }
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
                select();

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
            }
        }

        // metod koj go polni data grido kaj ucenik-profesor
        public void select()
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
                String konekcija = "server=localhost;Database=biblioteka_si;uid=root;pwd=root;";
                MySqlConnection conn = new MySqlConnection(konekcija);

                conn.Open();

                String komanda = "delete from ucenik where email='"+textBox7.Text+"'";
                MySqlCommand cmd = new MySqlCommand(komanda, conn);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Успешно бришење!");
                select();
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox11.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
