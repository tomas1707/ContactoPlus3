

using ContactosPlus.ApplicationCore;
using ContactosPlus.Domain;
using ContactosPlus.Infrastructure;

namespace ContactosPlus
{
    public partial class FrmDirectorioTelefonico : Form
    {
        private Directorio directorio;
        private Archivos miArchivo;
        public FrmDirectorioTelefonico()
        {
            InitializeComponent();
            directorio = new Directorio();
            miArchivo = new Archivos("Directorio.txt");
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
                                        Contacto contacto = new Contacto();
                                        contacto.nombre = txtNombre.Text.Trim();
                                        contacto.apellidos = txtApellidos.Text.Trim();
                                        contacto.telefono = txtTelefono.Text.Trim();
                                        contacto.extension = txtExtension.Text.Trim();
                                        contacto.puesto = txtCargo.Text.Trim();
                                        contacto.empresa = txtEmpresa.Text.Trim();
                                        contacto.correo = txtCorreo.Text.Trim();


                                        bool resultado = directorio.Add(contacto);

                                        if (resultado)
                                        {
                                            dgvDirectorio.DataSource = null;
                                            dgvDirectorio.DataSource = directorio.Show();
                                            //Agregar el contenido de la clase contacto al archivo
                                            miArchivo.GuardarDirectorio(directorio.Show());


                                            //Mensaje("Contacto guardado correctamente.");
                                            btnLimpiar_Click(sender, e);
                                        }
                                        else
                                            Mensaje("No fue posible guardar el contacto.","error");
                                        
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
            //No hay código implementado para la edición de registros en este momento.
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
                Mensaje("Registro eliminado");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //No se ha implementado la funcionalidad de búsqueda en este momento.
        }

        private void Mensaje(string texto, string tipo = "info")
        {
            MessageBoxIcon icono;

            switch (tipo.ToLower())
            {
                case "info":
                    icono = MessageBoxIcon.Information;
                    break;
                case "error":
                    icono = MessageBoxIcon.Error;
                    break;
                case "warning":
                    icono = MessageBoxIcon.Warning;
                    break;
                default:
                    icono = MessageBoxIcon.Information;
                    break;
            }

            MessageBox.Show(
                texto,
                "Directorio Telefónico",
                MessageBoxButtons.OK,
                icono
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
            //dgvDirectorio.AutoGenerateColumns = true;
            //dgvDirectorio.DataSource = directorio.Show();
            //dgvDirectorio.Columns["estatus"].Visible = false;

            //opción 2: Agregar columnas de manera manual
            dgvDirectorio.AutoGenerateColumns = false;
            dgvDirectorio.Columns.Clear();
            AgregarColumna("nombre", "Nombre Completo", 200);
            AgregarColumna("apellidos", "Apellidos", 200);
            AgregarColumna("telefono", "Teléfono", 200);
            AgregarColumna("extension", "Extensión", 100);
            AgregarColumna("puesto", "Cargo", 200);
            AgregarColumna("empresa", "Empresa", 200);
            AgregarColumna("correo", "Correo Electrónico", 200);
            dgvDirectorio.DataSource = directorio.Show();


            txtId.Text = directorio.GenerarId().ToString();
            txtNombre.Focus();
        }

        private void AgregarColumna(string dataPropertyName, string headerText, int width, bool visible = true, bool readOnly = true)
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

        private void dgvDirectorio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //lblPruebas.Text = $"Fila: {e.RowIndex}, Columna: {e.ColumnIndex}";
            Contacto? elemento = directorio.Find(e.RowIndex);
            lblPruebas.Text = elemento?.nombre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mensaje("Hola Mundo","error");
        }
    }
}
