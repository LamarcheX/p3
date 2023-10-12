using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace p3
{
    public partial class calificaciones : Form
    {
       
        public calificaciones()
        {
            InitializeComponent();
        }

        private void calcular_Click(object sender, EventArgs e)
        {
            int poe, pf, ap, va, pef, nf;
            poe = int.Parse(Npoe.Text);
            pf = int.Parse(Npf.Text);
            ap = int.Parse(Nap.Text);
            va = int.Parse(Nva.Text);
            pef = int.Parse(Npef.Text);
            nf = poe + pf + ap + va + pef;

            if (nf >= 90)
            {
                Cnf.Text = nf.ToString();
                L.Text = ("A");
                C.Text = ("Aprobado");
            }
            else if (nf >= 80)
            {
                Cnf.Text = Cnf.ToString();
                L.Text = ("B");
                C.Text = ("Aprobado");
            }
            else if (nf >= 70)
            {
                Cnf.Text = nf.ToString();
                L.Text = ("C");
                C.Text = ("Aprobado");
            }
            else if (nf >= 60)
            {
                Cnf.Text = nf.ToString();
                L.Text = ("F");
                C.Text = ("Reprobado");

            }
        }

        private void nuevo_Click(object sender, EventArgs e)
        {
            // 1. Incrementar
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
                this.codigo.Focus();
            }
            Conect.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(Cast(Mat_est as int)), 0)+1 from calificaciones", Conect.GetConnection());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            codigo.Text = dt.Rows[0][0].ToString();
            this.nombre.Focus();
            this.guardar.Enabled = true;


        }

        private void salir_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void guardar_Click(object sender, EventArgs e)
        {
            Conect.GetConnection();
            string insertar = "INSERT INTO Calificaciones (Mat_est, Nombre, poe, pf, ap, va, pef, L, C, Cnf) VALUES (@CODIGO, @NOMBRE, @NPOE, @NPF, @NAP, @NVA, @NPEF, @L, @C, @Cnf)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conect.GetConnection());
            cmd1.Parameters.AddWithValue("@CODIGO", codigo.Text);
            cmd1.Parameters.AddWithValue("@NOMBRE", nombre.Text);
            cmd1.Parameters.AddWithValue("@NPOE", Npoe.Text);
            cmd1.Parameters.AddWithValue("@NPF", Npf.Text);
            cmd1.Parameters.AddWithValue("@NAP", Nap.Text);
            cmd1.Parameters.AddWithValue("@NVA", Nva.Text);
            cmd1.Parameters.AddWithValue("@NPEF", Npef.Text);
            cmd1.Parameters.AddWithValue("@Cnf", Cnf.Text);
            cmd1.Parameters.AddWithValue("@L", L.Text);
            cmd1.Parameters.AddWithValue("@C", C.Text);

            cmd1.ExecuteNonQuery();

            MessageBox.Show("Los datos fueron agregados exitosamente");
            formReset();

        }

        private void formReset()
        {
            codigo.Clear();
            nombre.Clear();
            Npoe.Clear();
            Npf.Clear();
            Nap.Clear();
            Nva.Clear();
            Npef.Clear();
            Cnf.Clear();
            L.Clear();
            C.Clear();
            codigo.Focus();
        }

        private void changeTab_Nomina(object sender, EventArgs e)
        {
            Nomina nom = new Nomina();
            nom.Show();
            this.Dispose();
            this.Close();
        }

        private void codigo_KeyDown(object sender, KeyEventArgs e)
        {
            // 2. Programación para buscar
            if (e.KeyCode == Keys.Enter)
            {
                Conect.GetConnection();
                string cadsql = "select * from calificaciones where Mat_est=" + codigo.Text + "";
                SqlCommand comando = new SqlCommand(cadsql, Conect.GetConnection());

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.Read() == true)
                {
                    nombre.Text = leer["Nombre"].ToString();
                    Npoe.Text = leer["poe"].ToString();
                    Npf.Text = leer["pf"].ToString();
                    Nap.Text = leer["ap"].ToString();
                    Nva.Text = leer["va"].ToString();
                    Npef.Text = leer["pef"].ToString();
                    L.Text = leer["L"].ToString();
                    C.Text = leer["C"].ToString();
                    Cnf.Text = leer["Cnf"].ToString();
                }
            }
        //{
        //    // TODO: This line of code loads data into the 'p3DataSet.Calificaciones' table. You can move, or remove it, as needed.
        //   // this.calificacionesTableAdapter.Fill(this.p3DataSet.Calificaciones);

        //}
        }

        private void modificar_Click(object sender, EventArgs e)
        {
            // 3. Modificar o actualizar un registros
            Conect.GetConnection();
            string cadena = "update calificaciones set Nombre = @Nombre, poe = @Poe, pf = @Pf, ap = @Ap, va = @Va, pef = @Pef, L = @L, C = @C, Cnf = @Cnf Where Mat_est = @Codigo";

            SqlCommand comando = new SqlCommand(cadena, Conect.GetConnection());
            comando.Parameters.AddWithValue("@Nombre", nombre.Text);
            comando.Parameters.AddWithValue("@Poe", Npoe.Text);
            comando.Parameters.AddWithValue("@Pf", Npf.Text);
            comando.Parameters.AddWithValue("@Ap", Nap.Text);
            comando.Parameters.AddWithValue("@Va", Nva.Text);
            comando.Parameters.AddWithValue("@Pef", Npef.Text);
            comando.Parameters.AddWithValue("@L", L.Text);
            comando.Parameters.AddWithValue("@C", C.Text);
            comando.Parameters.AddWithValue("@Cnf", Cnf.Text);
            comando.Parameters.AddWithValue("@Codigo", codigo.Text);

            comando.ExecuteNonQuery();

            dataGridView1.DataSource = llenar_grid();

            formReset();
        }
        private void eliminar_Click(object sender, EventArgs e)
        {
            // 4.Eliminar registro
            if (MessageBox.Show("¿Eliminara el registro?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Conect.GetConnection();
                string eliminar = "DELETE FROM calificaciones WHERE Mat_est = @Mat_est" +
                    ""; // aqui solo estas indicando que vas a usar un parametro, en este caso
                SqlCommand cmd3 = new SqlCommand(eliminar, Conect.GetConnection());
                cmd3.Parameters.AddWithValue("@Mat_est", codigo.Text); // esta es la definicion del parametro
                cmd3.ExecuteNonQuery();
                dataGridView1.DataSource = llenar_grid();
                formReset();
            }

        }

        private void nombre_TextChanged(object sender, EventArgs e)
        {
            // 5. Busqueda Incremental
            Conect.GetConnection();
            DataTable dt = new DataTable();
            string consultar = "Select * from calificaciones where nombre like ('" + nombre.Text + "%')";
            SqlCommand cmd4 = new SqlCommand(consultar, Conect.GetConnection());
            cmd4.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd4);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public DataTable llenar_grid()
        {
            // 6. Actualizar el datagridview
            Conect.GetConnection();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM calificaciones order by Mat_est";
            SqlCommand cmd = new SqlCommand(consulta, Conect.GetConnection());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmCrystalRepCalificaciones RC = new frmCrystalRepCalificaciones();
            RC.Show();
        }
    }
}

