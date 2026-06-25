using ContactosPlus.Domain;
using ContactosPlus.Infrastructure;

namespace ContactosPlus.ApplicationCore
{
    internal class Directorio
    {
        private Archivos archivo;

        public Directorio(string rutaArchivo)
        {
            archivo = new Archivos(rutaArchivo);
        }

        // CREATE
        public bool Add(
            string nombre,
            string apellidos,
            string telefono,
            string extension,
            string puesto,
            string empresa,
            string correo,
            string observaciones)
        {
            int id = GenerarId();

            Contacto contacto = new Contacto(
                id,
                nombre,
                apellidos,
                telefono,
                extension,
                puesto,
                empresa,
                correo,
                observaciones);

            return archivo.Add(contacto);
        }

        // READ ALL
        public List<Contacto> Show()
        {
            return archivo.Show();
        }

        // READ ONE
        public Contacto? Find(int id)
        {
            return archivo.Find(id);
        }

        // UPDATE
        public bool Update(
            int id,
            string nombre,
            string apellidos,
            string telefono,
            string extension,
            string puesto,
            string empresa,
            string correo,
            string observaciones,
            bool estatus)
        {
            Contacto? contacto = archivo.Find(id);

            if (contacto == null)
                return false;

            contacto.nombre = nombre;
            contacto.apellidos = apellidos;
            contacto.telefono = telefono;
            contacto.extension = extension;
            contacto.puesto = puesto;
            contacto.empresa = empresa;
            contacto.correo = correo;
            contacto.observaciones = observaciones;
            contacto.estatus = estatus;

            return archivo.Update(contacto);
        }

        // DELETE
        public bool Delete(int id)
        {
            return archivo.Delete(id);
        }

        // Obtener siguiente ID
        public int GenerarId()
        {
            List<Contacto> lista = archivo.Show();

            if (lista.Count == 0)
                return 1;

            return lista.Max(c => c.id) + 1;
        }

        // Buscar por nombre
        public List<Contacto> BuscarPorNombre(string nombre)
        {
            return archivo.Show()
                          .Where(c =>
                              c.nombre.Contains(
                                  nombre,
                                  StringComparison.OrdinalIgnoreCase))
                          .ToList();
        }

        // Buscar por empresa
        public List<Contacto> BuscarPorEmpresa(string empresa)
        {
            return archivo.Show()
                          .Where(c =>
                              c.empresa.Contains(
                                  empresa,
                                  StringComparison.OrdinalIgnoreCase))
                          .ToList();
        }

        // Obtener solamente activos
        public List<Contacto> ObtenerActivos()
        {
            return archivo.Show()
                          .Where(c => c.estatus)
                          .ToList();
        }
    }
}