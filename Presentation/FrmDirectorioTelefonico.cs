

namespace ContactosPlus
{
    public partial class FrmDirectorioTelefonico : Form
    {
        public FrmDirectorioTelefonico()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarTexBox2(txtNombre))
                if (ValidarTexBox2(txtApellidos))
                    if (ValidarTexBox2(txtTelefono))
                        if (ValidarTexBox2(txtExtension))
                            if (ValidarTexBox2(txtCargo))
                                if (ValidarTexBox2(txtEmpresa))
                                    if (ValidarTexBox2(txtCorreo))
                                    {

                                    }
        }

        private bool ValidarTexBox(TextBox componente, string mensaje = "Campo Obligatorio")
        {
            string valor = componente.Text.Trim();

            if (valor.Length > 0)
            {
                error.SetError(componente, "");
                return true;
            }
            else
            {
                error.SetError(componente, mensaje);
                componente.Focus();
                return false;
            }
        }

        private bool ValidarTexBox2(TextBox componente, string mensaje = "Campo Obligatorio")
        {
            bool esValido = !string.IsNullOrWhiteSpace(componente.Text.Trim());

            error.SetError(componente, esValido ? "" : mensaje);

            if (!esValido)
                componente.Focus();

            return esValido;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Registro actualizado.",
                "Directorio Telefónico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Desea eliminar el registro?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Registro eliminado.",
                    "Directorio Telefónico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Implementar búsqueda.",
                "Directorio Telefónico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        List<TextBox> componentes = new List<TextBox>();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach (TextBox componente in componentes)
                componente.Clear();

            chkActivo.Checked = true;
            txtNombre.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnLimpiar_Click(sender, e);
        }

        private void FrmDirectorioTelefonico_Load(object sender, EventArgs e)
        {

        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("Queeee me cambiaste por la otra");
        }

        private void txtApellidos_Leave(object sender, EventArgs e)
        {

        }
    }
}
