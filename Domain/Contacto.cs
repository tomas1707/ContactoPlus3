

namespace ContactosPlus.Domain
{
    internal class Contacto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string extension { get; set; }
        public string puesto { get; set; }
        public string empresa { get; set; }
        public string correo { get; set; }
        public string observaciones { get; set; }
        public bool estatus { get; set; }

        public Contacto()
        {
            id = 0;
            nombre = "";
            apellidos = "";
            telefono = "0";
            extension = "NA";
            puesto = "";
            empresa = "";
            correo = "";
            observaciones = "";
            estatus = true;
        }

        public Contacto(int id, string nombre, string apellidos, string telefono, string extension, string puesto, string empresa, string correo, string observaciones)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.extension = extension;
            this.empresa = empresa;
            this.puesto = puesto;
            this.correo = correo;
            this.observaciones = observaciones;
            this.estatus = true;
        }

        public override string ToString()
        {
            return $"{id}|{nombre}|{apellidos}|{telefono}|{extension}|{puesto}|{empresa}|{correo}|{observaciones}|{estatus}";
        }


    }

}
