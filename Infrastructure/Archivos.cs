using ContactosPlus.Domain;



namespace ContactosPlus.Infrastructure
{

    internal class Archivos {

        private string rutaArchivo;

        public Archivos(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;
        }

        public bool add(Contacto contacto)
        {
            try
            {
                StreamWriter escritor = new StreamWriter(rutaArchivo, true);
                escritor.WriteLine(contacto);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // READ ALL
        public List<Contacto> show()
        {
            List<Contacto> lista = new List<Contacto>();

            StreamReader lector = new StreamReader(rutaArchivo);

            string? linea;

            while ((linea = lector.ReadLine()) != null)
            {

                //string mismacotas = "cofee | chuky | lila | coco | chirgo";
                //["cofee", "chu","ky", "lila", "coco", "chirgo"]

                //
                string[] datos = linea.Split('|');

                //***(int id,
                //string nombre,
                //string apellidos,
                //int telefono,
                //int extension,
                //string puesto,
                //string empresa,
                //string correo,
                //string observaciones)

                Contacto contacto = new Contacto(
                    Convert.ToInt32(datos[0]), //int id
                    datos[1], //string nombre
                    datos[2], //string apellidos
                    Convert.ToInt32(datos[3]), //int telefono
                    Convert.ToInt32(datos[4]), //int extension
                    datos[5], //strig puesto
                    datos[6], //string empresa
                    datos[7], //string correo
                    datos[8] //string observaciones
                );

                contacto.observaciones = datos[9];
                contacto.estatus = Convert.ToBoolean(datos[10]);

                lista.Add(contacto);
            }

            return lista;
        }

        // READ ONE
        public Contacto? find(int id)
        {

            List<Contacto> directorio = show();

            foreach (Contacto contacto in directorio)
                if (contacto.id == id)
                    return contacto;

            return null;
        }

        // UPDATE
        public bool update(Contacto contacto)
        {
            try
            {
                List<Contacto> lista = show();

                Contacto? actual = lista.FirstOrDefault(c => c.id == contacto.id);

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
        public bool delete(int id)
        {
            try
            {
                List<Contacto> lista = show();

                Contacto contactoEncontrado = null;

                foreach (Contacto contacto in lista)
                {
                    if (contacto.id == id)
                    {
                        contactoEncontrado = contacto;
                        break;
                    }
                }

                if (contactoEncontrado == null)
                    return false;

                lista.Remove(contactoEncontrado);

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
            using StreamWriter escritor = new StreamWriter(rutaArchivo, false);

            foreach (Contacto contacto in lista)
            {
                escritor.WriteLine(contacto);
            }
        }

    }
    
}


