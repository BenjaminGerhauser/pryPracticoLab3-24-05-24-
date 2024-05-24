using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryPracticoLab3_24_05_24_
{
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
        }
        clsUsuario usuario = new clsUsuario();
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frmReportes reportes = new frmReportes();
            if (usuario.registrarUser(txtContraseña.Text, txtUsuario.Text, cboCategoria.Text) == true)
            {
                this.Hide();
                reportes.ShowDialog();
            }
            else MessageBox.Show("Usuario existente");
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cboCategoria.SelectedIndex = -1;
            txtContraseña.Text = "";
            txtUsuario.Text = "";
            frmLogin login = new frmLogin();
            this.Hide();
            login.ShowDialog();
        }
    }
}
