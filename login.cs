using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p3
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{}");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Conect.GetConnection();
            string consulta = "SELECT * FROM Usuario WHERE usuNombre = '" + txtUsuario.Text + "'and usuClave = '" + txtClave.Text + "'";
            SqlCommand comando = new SqlCommand(consulta, Conect.GetConnection());
            SqlDataReader lector;
            lector = comando.ExecuteReader();

            if (lector.HasRows == true)
            {
                MessageBox.Show("Bienvenido al sistema");
                menu menu = new menu();
                menu.Show();
                this.Hide();
                this.txtUsuario.Text = "";
                this.txtClave.Text = "";
                this.txtUsuario.Focus();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");
                this.txtUsuario.Text = "";
                this.txtClave.Text = "";
                this.txtUsuario.Focus();
            }
        }
    }
}
