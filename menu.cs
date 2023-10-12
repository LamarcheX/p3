using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p3
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void nomina_CheckedChanged(object sender, EventArgs e)
        {
            if (nomina.Checked)
            {
                Nomina frm = new Nomina();
                frm.Show();
                this.Hide();
            }

        }

        private void calificaciones_CheckedChanged(object sender, EventArgs e)
        {
            if (calificaciones.Checked)
            {
                calificaciones frm = new calificaciones();
                frm.Show();
                this.Hide();
            }
        }

        private void registro_CheckedChanged(object sender, EventArgs e)
        {
            if (registro.Checked)
            {
                Registrar_Usuario frm = new Registrar_Usuario();
                frm.Show();
                this.Hide();
            }
        }
    }
    
}