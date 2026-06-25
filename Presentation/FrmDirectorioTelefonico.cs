

using ContactosPlus.ApplicationCore;

namespace ContactosPlus
{
    public partial class FrmDirectorioTelefonico : Form
    {
        private Directorio directorio;
        public FrmDirectorioTelefonico()
        {
            InitializeComponent();
            directorio = new Directorio("contactos.txt");
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
                                        {
                                            bool resultado = directorio.Add(
                                                txtNombre.Text.Trim(),
                                                txtApellidos.Text.Trim(),
                                                txtTelefono.Text.Trim(),
                                                txtExtension.Text.Trim(),
                                                txtCargo.Text.Trim(),
                                                txtEmpresa.Text.Trim(),
                                                txtCorreo.Text.Trim(),
                                                txtObservaciones.Text.Trim()
                                            );

                                            if (resultado)
                                            {
                                                dgvDirectorio.DataSource = null;
                                                dgvDirectorio.DataSource = directorio.Show();

                                                MessageBox.Show(
                                                    "Contacto guardado correctamente.",
                                                    "Directorio Telefónico",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information
                                                );

                                                btnLimpiar_Click(sender, e);
                                            }
                                            else
                                            {
                                                MessageBox.Show(
                                                    "No fue posible guardar el contacto.",
                                                    "Directorio Telefónico",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error
                                                );
                                            }
                                        }
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
            componentes.Add(txtId);
            componentes.Add(txtNombre);
            componentes.Add(txtApellidos);
            componentes.Add(txtTelefono);
            componentes.Add(txtExtension);
            componentes.Add(txtCargo);
            componentes.Add(txtEmpresa);
            componentes.Add(txtCorreo);

            //opción 1: Agregare de manera directa las columnas al DataGridView
            dgvDirectorio.AutoGenerateColumns = true;
            dgvDirectorio.DataSource = directorio.Show();

            dgvDirectorio.Columns["estatus"].Visible = false;

            //opción 2: Agregar columnas de manera manual
            //dgvDirectorio.AutoGenerateColumns = false;
            //dgvDirectorio.Columns.Clear();
            //AgregarColumnaTexto("nombre", "Nombre Completo", 200);


            txtId.Text = directorio.GenerarId().ToString();
            txtNombre.Focus();
        }

        private void AgregarColumnaTexto(string dataPropertyName, string headerText, int width, bool visible = true, bool readOnly = true)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = dataPropertyName;
            col.HeaderText = headerText;
            col.Width = width;
            col.Visible = visible;
            col.ReadOnly = readOnly;

            // Opcional: Centrar encabezados o aplicar estilos por defecto aquí
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvDirectorio.Columns.Add(col);
        }
    }
}
