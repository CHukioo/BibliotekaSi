using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            cmd.Parameters.AddWithValue("@profesor", textBox6.Text);

            cmd.ExecuteNonQuery();

            conn.Close();
                MessageBox.Show("Успешен внес за: "+textBox1.Text+" "+textBox2.Text);
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
