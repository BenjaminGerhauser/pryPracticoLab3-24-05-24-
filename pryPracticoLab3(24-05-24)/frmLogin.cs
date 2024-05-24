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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        clsUsuario usuario = new clsUsuario();

        private void btnInicio_Click(object sender, EventArgs e)
        {
            frmReportes reportes = new frmReportes();
            string nombreUser = txtUsuario.Text;
            string passUser = txtUsuario.Text;
            if (usuario.ValidarUsuario(nombreUser, passUser) == true) 
            {
                usuario.RegistroLogInicioSesion();
                this.Hide(); 
                reportes.ShowDialog(); 
            }
            else MessageBox.Show("Usuario o contraseña incorrectos");
            
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frmRegistro registro = new frmRegistro();
            this.Hide();
            registro.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtContraseña.Text = "";
            txtUsuario.Text = "";
        }
    }
}
