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
    public partial class Nomina : Form
    {
        public Nomina()
        {
            InitializeComponent();
        }

        private void salir_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            this.Close();
            menu.Show();

        }

        private void calcular_Click(object sender, EventArgs e)
        {
            decimal afp, ars, sb;
            decimal arsd = (decimal)(0.0291);
            decimal afpd = (decimal)(0.0304);
            sb = Convert.ToDecimal(sbase.Text);

            afp = sb * afpd;
            ars = sb * arsd;
            afpdes.Text = afp.ToString();
            arsdes.Text = ars.ToString();

            decimal pa = (decimal)(33384);
            decimal po = (decimal)(0.15);
            decimal isrd;
            decimal tdd;
            decimal sn;

            if (sb > pa)
            {
                isrd = (sb - pa) * po;
            }
            else
            {
                isrd = 0;
            }
            tdd = afp + ars + isrd;
            sn = sb - tdd;

            isrdes.Text = isrd.ToString();
            tdes.Text = tdd.ToString();
            sneto.Text = sn.ToString();

        }

        private void guardar_Click(object sender, EventArgs e)
        {
            Conect.GetConnection();
            string insertar = "INSERT INTO Nomina (cod_emp, Nombre, Sueldo_base, AFP, ARS, ISR, Tdesc, Sueldo_neto)VALUES(@CODIGO, @NOMBRE, @SUELDO, @AFP, @ARS, @ISR, @TDESC, @SNETO)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conect.GetConnection());
            cmd1.Parameters.AddWithValue("@CODIGO", txtcodigo.Text);
            cmd1.Parameters.AddWithValue("@NOMBRE", nombre.Text);
            cmd1.Parameters.AddWithValue("@SUELDO", sbase.Text);
            cmd1.Parameters.AddWithValue("@AFP", afpdes.Text);
            cmd1.Parameters.AddWithValue("@ARS", arsdes.Text);
            cmd1.Parameters.AddWithValue("@ISR", isrdes.Text);
            cmd1.Parameters.AddWithValue("@TDESC", tdes.Text);
            cmd1.Parameters.AddWithValue("@SNETO", sneto.Text);

            cmd1.ExecuteNonQuery();

            MessageBox.Show("Los datos fueron agregados exitosamente");
            txtcodigo.Focus();
            FormReset();

        }

        private void FormReset()
        {
            txtcodigo.Clear();
            nombre.Clear();
            sbase.Clear();
            afpdes.Clear();
            arsdes.Clear();
            isrdes.Clear();
            tdes.Clear();
            sneto.Clear();
            txtcodigo.Focus();
        }

        private void changeTab_Nomina(object sender, EventArgs e)
        {
            calificaciones cal = new calificaciones();
            cal.Show();
            this.Dispose();
            this.Close();
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
                this.txtcodigo.Focus();
            }
            Conect.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(Cast(cod_emp as int)), 0)+1 from nomina", Conect.GetConnection());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtcodigo.Text = dt.Rows[0][0].ToString();
            this.nombre.Focus();
            this.guardar.Enabled = true;

        }

        private void txtcodigo_KeyDown(object sender, KeyEventArgs e)
        {
            // 2. Programación para buscar
            if (e.KeyCode == Keys.Enter)
            {
                Conect.GetConnection();
                string cadsql = "select * from nomina where cod_emp=" + txtcodigo.Text + "";
                SqlCommand comando = new SqlCommand(cadsql, Conect.GetConnection());

                SqlDataReader leer = comando.ExecuteReader();
                if (leer.Read() == true)
                {
                    nombre.Text = leer["Nombre"].ToString();
                    sbase.Text = leer["Sueldo_base"].ToString();
                    afpdes.Text = leer["AFP"].ToString();
                    arsdes.Text = leer["ARS"].ToString();
                    isrdes.Text = leer["ISR"].ToString();
                    tdes.Text = leer["Tdesc"].ToString();
                    sneto.Text = leer["Sueldo_neto"].ToString();
                }
            }
        }

        private void modificar_Click(object sender, EventArgs e)
        {
            Conect.GetConnection();
            string modificar = "UPDATE nomina SET Nombre = @NOMBRE, Sueldo_base = @SUELDO_BASE, AFP = @AFP, ARS = @ARS, ISR = @ISR, Tdesc = @TDESC, Sueldo_neto = @SUELDO_NETO WHERE COD_EMP = @COD_EMP";

            SqlCommand cmd = new SqlCommand(modificar, Conect.GetConnection());
            cmd.Parameters.AddWithValue("@COD_EMP", txtcodigo.Text);
            cmd.Parameters.AddWithValue("@NOMBRE", nombre.Text);
            cmd.Parameters.AddWithValue("@SUELDO_BASE", sbase.Text);
            cmd.Parameters.AddWithValue("@AFP", afpdes.Text);
            cmd.Parameters.AddWithValue("@ARS", arsdes.Text);
            cmd.Parameters.AddWithValue("@ISR", isrdes.Text);
            cmd.Parameters.AddWithValue("@TDESC", tdes.Text);
            cmd.Parameters.AddWithValue("@SUELDO_NETO", sneto.Text);

            cmd.ExecuteNonQuery();

            dataGridView1.DataSource = llenar_grid();

            FormReset();
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            // 4.Eliminar registro
            if (MessageBox.Show("¿Eliminara el registro?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Conect.GetConnection();
                string eliminar = "DELETE FROM nomina WHERE cod_emp = @cod_emp"; // aqui solo estas indicando que vas a usar un parametro, en este caso: @cod_emp
                SqlCommand cmd3 = new SqlCommand(eliminar, Conect.GetConnection());
                cmd3.Parameters.AddWithValue("@cod_emp", txtcodigo.Text); // esta es la definicion del parametro
                cmd3.ExecuteNonQuery();
                dataGridView1.DataSource = llenar_grid();
            }
        }

        private void nombre_TextChanged(object sender, EventArgs e)
        {
            // 5. Busqueda Incremental
            Conect.GetConnection();
            DataTable dt = new DataTable();
            string consultar = "Select * from nomina where nombre like ('" + nombre.Text + "%')";
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
            string consulta = "SELECT * FROM nomina order by cod_emp desc";
            SqlCommand cmd = new SqlCommand(consulta, Conect.GetConnection());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            LN NL = new LN();
            NL.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}