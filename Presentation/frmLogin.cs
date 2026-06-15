using ContactosPlus.Shared;

namespace ContactosPlus
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (usuario.Length > 0)
            {

                if (pass.Length > 0)
                {
                     if (usuario=="admin" && pass=="1234")
                    {
                        // Abrimos la siguiente ventana (FrmDirectorioTelefonico)
                       // MisFunciones.mensajeAlerta("Bienvenido al sistemas");
                       FrmDirectorioTelefonico obj = new FrmDirectorioTelefonico();

                        this.Hide();// ocultar login
                        obj.Show(); // mostrar contactos
                        obj.FormClosed += (s, args) => this.Show();
                    }
                    else {
                        MisFunciones.mensajeAlerta("Verifica tus datos");
                        txtUsuario.Text = "";
                        txtPassword.Text = "";
                        txtUsuario.Focus();
                    }
                }
                else {
                   MisFunciones.mensajeAlerta("Agrega tu contraseña");
                    txtPassword.Focus();
                }
            }
            else {
                MisFunciones.mensajeAlerta("Agrega tu usuario");
                txtUsuario.Focus();
            }

        
        }
      
    }
}
       


      
      




