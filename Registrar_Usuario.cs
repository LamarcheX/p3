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

namespace p3
{
    public partial class Registrar_Usuario : Form
    {
        public Registrar_Usuario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //validar campos en blanco
            if (txtUsuario.Text == string.Empty)
            {
                MessageBox.Show("El campo Usuario es obligatorio");
                this.txtUsuario.Focus();
            }
            else if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("El campo Clave es obligatorio");
                this.txtClave.Focus();
            }
            else if (txtConfClave.Text == string.Empty)
            {
                MessageBox.Show("Confirme la clave");
                this.txtConfClave.Focus();
            }
            else if (txtClave.Text != txtConfClave.Text)
            {
                MessageBox.Show("Las claves no coinciden");
                this.txtClave.Text = "";
                this.txtConfClave.Text = "";
                this.txtClave.Focus();
            }
            else
            {
                //guardar usuario nuevo
                Conect.GetConnection();
                string ingresarUsuarioNuevo = "INSERT INTO Usuario (usuCodigo, usuNombre, usuClave, usuConfirmClave, usuCelular, usuCorreo) VALUES (@usuCodigo, @usuNombre, @usuClave, @usuConfirmClave, @usuCelular, @usuCorreo)";
                SqlCommand cmd = new SqlCommand(ingresarUsuarioNuevo, Conect.GetConnection());
                cmd.Parameters.AddWithValue("@usuCodigo", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@usuNombre", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@usuClave", txtClave.Text);
                cmd.Parameters.AddWithValue("@usuConfirmClave", txtConfClave.Text);
                cmd.Parameters.AddWithValue("@usuCelular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@usuCorreo", txtCorreo.Text);

                cmd.ExecuteNonQuery();
                dataGridView1.DataSource = llenar_grid();
                MessageBox.Show("Datos agregados con exito");
                this.btnGuardar.Enabled = false;
            }
        }
        public DataTable llenar_grid()
        {
            // 6. Actualizar el datagridview
            Conect.GetConnection();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM Usuario order by usuCodigo";
            SqlCommand cmd = new SqlCommand(consulta, Conect.GetConnection());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Incrementar
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
                this.txtCodigo.Focus();
            }
            Conect.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(Cast(usuCodigo as int)), 0)+1 from usuario", Conect.GetConnection());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtCodigo.Text = dt.Rows[0][0].ToString();
            this.txtUsuario.Focus();
            this.btnGuardar.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Conect.GetConnection();
            string insertar = "INSERT INTO usuario (usuCodigo, usuNombre, usuClave, usuConfirmClave, usuCelular, usuCorreo) VALUES (@usuCodigo, @usuNombre, @usuClave, @usuConfirmClave, @usuCelular, @usuCorreo)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conect.GetConnection());
            cmd1.Parameters.AddWithValue("@usuCodigo", txtCodigo.Text);
            cmd1.Parameters.AddWithValue("@usuNombre", txtUsuario.Text);
            cmd1.Parameters.AddWithValue("@usuClave", txtClave.Text);
            cmd1.Parameters.AddWithValue("@usuConfirmClave", txtConfClave.Text);
            cmd1.Parameters.AddWithValue("@usuCelular", txtCelular.Text);
            cmd1.Parameters.AddWithValue("@usuCorreo", txtCorreo.Text);


            cmd1.ExecuteNonQuery();

            MessageBox.Show("Los datos fueron agregados exitosamente");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            {
                // 4.Eliminar registro
                if (MessageBox.Show("¿Eliminara el registro?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Conect.GetConnection();
                    string eliminar = "DELETE FROM usuario WHERE usuCodigo = @txtCodigo"; // aqui solo estas indicando que vas a usar un parametro, en este caso: @cod_emp
                    SqlCommand cmd3 = new SqlCommand(eliminar, Conect.GetConnection());
                    cmd3.Parameters.AddWithValue("@usuCodigo", txtCodigo.Text); // esta es la definicion del parametro
                    cmd3.ExecuteNonQuery();
                    dataGridView1.DataSource = llenar_grid();
                }

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
