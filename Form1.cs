using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TicTacToe
{
    
    public partial class Form1 : Form
    {

        SqlConnection conexion = new SqlConnection("server = DESKTOP-D8ULQLI; database = Juegos; integrated security = true");

        string JugadorX = "";
        string JugadorO = "";
        bool cambio = true;
        int empate = 0;
        int ganadasX = 0;
        int ganadasO = 0;
        public Form1()
        {
            InitializeComponent();
        }

       


        private void Form1_Load(object sender, EventArgs e)
        {
            OnOffBtn(false);    
        }

        private void OnOffBtn(bool onoff)
        {
            a1.Enabled = onoff;
            a2.Enabled = onoff;
            a3.Enabled = onoff;
            b1.Enabled = onoff;
            b2.Enabled = onoff;
            b3.Enabled = onoff;
            c1.Enabled = onoff;
            c2.Enabled = onoff;
            c3.Enabled = onoff;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           

            string query = "INSERT INTO Tictactoe (Jugador_1,Jugador_2) VALUES (@jugador_1,@jugador_2)";
            conexion.Open();
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@jugador_1", txtUser1.Text);
            comando.Parameters.AddWithValue("@jugador_2", txtUser2.Text);
            comando.ExecuteNonQuery();
            

            Ingresar();
        }

        private void Ingresar()
        {
            if (txtUser1.Text == "" && txtUser2.Text == "")
            {
                MessageBox.Show("El nombre de los jugadores no debe estar vacio", "Nombre no valido", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if(txtUser1.Text == "")
                {
                    MessageBox.Show("El nombre del jugador 1 no debe estar vacio", "Nombre no valido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                if (txtUser2.Text == "")
                {
                    MessageBox.Show("El nombre del jugador 2 no debe estar vacio", "Nombre no valido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            if (txtUser1.Text != "" && txtUser2.Text != "")
            {
                if (rbtnUser1X.Checked && rbtnUser2O.Checked)
                {
                    JugadorX = txtUser1.Text;
                    JugadorO = txtUser2.Text;
                    rbtnUser1O.Enabled = false;
                    rbtnUser2X.Enabled = false;
                    PlayGame();
                }
                if (rbtnUser1O.Checked && rbtnUser2X.Checked)
                {
                    JugadorX = txtUser2.Text;
                    JugadorO = txtUser1.Text;
                    rbtnUser1X.Enabled = false;
                    rbtnUser2O.Enabled = false;
                    PlayGame();
                }
                if (rbtnUser1X.Checked && rbtnUser2X.Checked)
                {
                    MessageBox.Show("Solo un jugador puede seleccionar la letra X.", "Vuelva a escoger la letra", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                if (rbtnUser1O.Checked && rbtnUser2O.Checked)
                {
                    MessageBox.Show("Solo un jugador puede seleccionar la letra O.", "Vuelva a escoger la letra", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                if (rbtnUser1X.Checked == false && rbtnUser1O.Checked == false || rbtnUser2X.Checked == false && rbtnUser2O.Checked == false)
                {
                    MessageBox.Show("Cada jugador debe seleccionar una letra.", "Vuelva a escoger la letra", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void PlayGame()
        {
            lblUser1.Text = txtUser1.Text;
            lblUser2.Text = txtUser2.Text;

            lblUserPoints1.Visible = true;
            lblUserPoints2.Visible = true;

            groupBox1.Text = "Marcador";

            btnClean.Visible = true;
            btnRestart.Visible = true;

            btnStart.Visible = false;
            txtUser1.Visible = false;
            txtUser2.Visible = false;

            MessageBox.Show("Empieza " + JugadorX, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnOffBtn(true);
        }

        private void Buttons_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (cambio)
            {
                b.Text = "X";

            }
            else
            {
                b.Text = "O";
            }
            cambio = !cambio;
            b.Enabled = false;
            Partida();
        }

        private void Partida()
        {
            if ((a1.Text == a2.Text) & (a2.Text == a3.Text) && (!a1.Enabled))
            {
                Validacion(a1);
            }
            else
            if ((b1.Text == b2.Text) & (b2.Text == b3.Text) && (!b1.Enabled))
            {
                Validacion(b1);
            }
            else
            if ((c1.Text == c2.Text) & (c2.Text == c3.Text) && (!c1.Enabled))
            {
                Validacion(c1);
            }




            if ((a1.Text == b1.Text) & (b1.Text == c1.Text) && (!a1.Enabled))
            {
                Validacion(a1);
            }
            else
             if ((a2.Text == b2.Text) & (b2.Text == c2.Text) && (!a2.Enabled))
            {
                Validacion(a2);
            }
            else
             if ((a3.Text == b3.Text) & (b3.Text == c3.Text) && (!a3.Enabled))
            {
                Validacion(a3);
            }


            if ((a1.Text == b2.Text) & (b2.Text == c3.Text) && (!a1.Enabled))
            {
                Validacion(a1);
            }
            else
            if ((a3.Text == b2.Text) & (b2.Text == c1.Text) && (!a3.Enabled))
            {
                Validacion(a3);
            }

            empate++;
            if (empate == 9)
            {
                MessageBox.Show("Es un empate" , "Empate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clean();
                OnOffBtn(true);
                empate = 0;
            }
        }
        public void Validacion(Button b)
        {
            empate = -1;
            
            if(b.Text == "X")
            {
                MessageBox.Show("Gana " + JugadorX , "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ganadasX++;
            }
            else if (b.Text == "X")
            {
                MessageBox.Show("Gana " + JugadorO , "Felicidades", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ganadasO++;
            }
            if(rbtnUser1X.Checked && rbtnUser2O.Checked)
            {
                lblUserPoints1.Text = ganadasX.ToString();
                lblUserPoints2.Text = ganadasO.ToString();
            }
            if (rbtnUser1O.Checked && rbtnUser2X.Checked)
            {
                lblUserPoints2.Text = ganadasX.ToString();
                lblUserPoints1.Text = ganadasO.ToString();
            }
            Clean();
            OnOffBtn(true);
        }

        private void Clean()
        {
            a1.Text = "";
            a2.Text = "";
            a3.Text = "";
            b1.Text = "";
            b2.Text = "";
            b3.Text = "";
            c1.Text = "";
            c2.Text = "";
            c3.Text = "";

        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            Clean();
            OnOffBtn(true);
            empate = 0;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Clean();
            OnOffBtn(false);
            btnClean.Visible = false;
            btnRestart.Visible = false;

            btnStart.Visible = true;
            txtUser2.Visible = true;
            txtUser1.Visible = true;

            JugadorX = "";
            JugadorO = "";
            ganadasX = 0;
            ganadasO = 0;
            cambio = true;

            lblUserPoints1.Text = 0.ToString();
            lblUserPoints2.Text = 0.ToString();
            lblUser1.Text = "";
            lblUser2.Text = "";

            rbtnUser1O.Enabled = true;
            rbtnUser2X.Enabled = true;
            rbtnUser1X.Enabled = true;
            rbtnUser2O.Enabled = true;

            rbtnUser1X.Checked = false;
            rbtnUser1O.Checked = false;
            rbtnUser2X.Checked = false;
            rbtnUser2O.Checked = false;

            lblUserPoints1.Visible = false;
            lblUserPoints2.Visible = false;

            groupBox1.Text = "WRITE THE NAMES OF THE PLAYERS...";

        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("Select * from Tictactoe", conexion);
            SqlDataAdapter Adaptador = new SqlDataAdapter();
            Adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            Adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla; 



          


        }
    }
}
