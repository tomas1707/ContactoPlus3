using ContactosPlus.Domain;

namespace ContactosPlus.Infrastructure
{
    internal class Archivos
    {
        private string rutaArchivo;

        public Archivos(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;

            // Crear el archivo si no existe
            if (!File.Exists(rutaArchivo))
            {
                File.Create(rutaArchivo).Close();
            }
        }

        // CREATE
        public bool Add(Contacto contacto)
        {
            try
            {
                using StreamWriter escritor =
                    new StreamWriter(rutaArchivo, true);

                escritor.WriteLine(contacto);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // READ ALL
        public List<Contacto> Show()
        {
            List<Contacto> lista = new List<Contacto>();

            try
            {
                using StreamReader lector =
                    new StreamReader(rutaArchivo);

                string? linea;

                while ((linea = lector.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split('|');

                    if (datos.Length != 10)
                        continue;

                    Contacto contacto = new Contacto(
                        Convert.ToInt32(datos[0]),
                        datos[1],
                        datos[2],
                        datos[3],
                        datos[4],
                        datos[5],
                        datos[6],
                        datos[7],
                        datos[8]
                    );

                    contacto.estatus =
                        Convert.ToBoolean(datos[9]);

                    lista.Add(contacto);
                }
            }
            catch
            {
            }

            return lista;
        }

        // READ ONE
        public Contacto? Find(int id)
        {
            List<Contacto> lista = Show();

            return lista.FirstOrDefault(
                c => c.id == id);
        }

        // UPDATE
        public bool Update(Contacto contacto)
        {
            try
            {
                List<Contacto> lista = Show();

                Contacto? actual =
                    lista.FirstOrDefault(
                        c => c.id == contacto.id);

                if (actual == null)
                    return false;

                actual.nombre = contacto.nombre;
                actual.apellidos = contacto.apellidos;
                actual.telefono = contacto.telefono;
                actual.extension = contacto.extension;
                actual.puesto = contacto.puesto;
                actual.empresa = contacto.empresa;
                actual.correo = contacto.correo;
                actual.observaciones = contacto.observaciones;
                actual.estatus = contacto.estatus;

                GuardarLista(lista);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            try
            {
                List<Contacto> lista = Show();

                Contacto? contacto =
                    lista.FirstOrDefault(
                        c => c.id == id);

                if (contacto == null)
                    return false;

                lista.Remove(contacto);

                GuardarLista(lista);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Método privado
        private void GuardarLista(List<Contacto> lista)
        {
            using StreamWriter escritor =
                new StreamWriter(rutaArchivo, false);

            foreach (Contacto contacto in lista)
            {
                escritor.WriteLine(contacto);
            }
        }
    }
}