using ContactosPlus.Domain;

namespace ContactosPlus.Infrastructure
{
    internal class Archivos
    {
        private string rutaArchivo;
        private string msgError;

        //Constructor de la clase Archivos
        public Archivos(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;

            // Crear el archivo si no existe
            if (!File.Exists(rutaArchivo))
            {
                File.Create(rutaArchivo).Close();
            }
        }

        //Método para guardar un contacto al archivo a partor de los datos de la clase Contacto
        public bool GuardarContacto(Contacto contacto)
        {
            try
            {
                using StreamWriter escritor =  new StreamWriter(rutaArchivo, true);
                escritor.WriteLine(contacto);
                return true;
            }
            catch (Exception e) 
            {
                msgError = e.Message;
                return false;
            }
        }

        //Método para guardar en el archivo, todos los contactor que estén registrados en el directorio
        public void GuardarDirectorio(List<Contacto> directorio)
        {
            try
            {
                using StreamWriter escritor = new StreamWriter(rutaArchivo, false);
            }
            catch (Exception e)
            {
                msgError = e.Message;
            }

        }

        //Método que extrae todos los contactos del archivo y lo devuelve en la lista directorio.
        public List<Contacto> MostrarDirectorio()
        {
            List<Contacto> lista = new List<Contacto>();

            try
            {
                using StreamReader lector = new StreamReader(rutaArchivo);

                string? linea;

                while ((linea = lector.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split('|');

                    if (datos.Length != 10)
                        continue;

                    Contacto contacto = new Contacto(
                        Convert.ToInt32(datos[0]), //int id
                        datos[1], //string nombre
                        datos[2], //string apellidos
                        datos[3], //string telefono
                        datos[4], //string extension
                        datos[5], //string puesto
                        datos[6], //string empresa
                        datos[7], //string correo
                        datos[8] //string observaciones 
                    );

                    contacto.estatus = Convert.ToBoolean(datos[9]);

                    lista.Add(contacto);
                }
            }
            catch (Exception e)
            {
                msgError = e.Message;
            }

            return lista;
        }

        //Método que extrae todos los contactos del archivo y los carga en la lista directorio
        //Posteriormente buscar el conacto, lo actualiza y finalmente guarda los cambios en el archivo.
        public bool ActualizarContacto(Contacto contacto)
        {
            try
            {
                List<Contacto> directorio = MostrarDirectorio();

                Contacto? actual = directorio.FirstOrDefault(c => c.id == contacto.id);

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

                GuardarDirectorio(directorio);

                return true;
            }
            catch (Exception e)
            {
                msgError = e.Message;
                return false;
            }
        }

        //Método para eliminar un contacto del archivo
        public bool EliminarContacto(int id)
        {
            try
            {
                List<Contacto> directorio = MostrarDirectorio();

                Contacto? contacto = directorio.FirstOrDefault( c => c.id == id);

                if (contacto == null)
                    return false;

                directorio.Remove(contacto);

                GuardarDirectorio(directorio);

                return true;
            }
            catch (Exception e)
            {
                msgError = e.Message;
                return false;
            }
        }

        //Método para eliminar el archivo que contiene el directorio de contactos.
        public bool EliminarArchivo()
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                msgError = e.Message;
                return false;
            }
        }

        //Método para renombrar el archivo del directorio de contacto.
        public bool RenombrarArchivo(string nuevoNombre)
        {
            try
            {
                if (!File.Exists(rutaArchivo)) return false;

                string? directorio = Path.GetDirectoryName(rutaArchivo);
                string nuevaRuta = Path.Combine(directorio ?? "", nuevoNombre);

                File.Move(rutaArchivo, nuevaRuta);
                rutaArchivo = nuevaRuta; // Actualizamos el estado interno de la clase
                return true;
            }
            catch (Exception e)
            {
                msgError = e.Message;
                return false;
            }
        }

        //Método para mover de ruta, el archivo del directorio de contactos.
        public bool MoverArchivo(string nuevaRutaCompleta)
        {
            try
            {
                if (!File.Exists(rutaArchivo)) return false;

                // Asegurar que el directorio de destino exista
                string? nuevoDirectorio = Path.GetDirectoryName(nuevaRutaCompleta);
                if (!string.IsNullOrEmpty(nuevoDirectorio) && !Directory.Exists(nuevoDirectorio))
                {
                    Directory.CreateDirectory(nuevoDirectorio);
                }

                File.Move(rutaArchivo, nuevaRutaCompleta);
                rutaArchivo = nuevaRutaCompleta; // Actualizamos la referencia interna
                return true;
            }
            catch (Exception e)
            {
                msgError = e.Message;
                return false;
            }
        }

        //Método para extraer el mensaje de error en caso de que exista alguno.
        public string getError()
        {
            return this.msgError;
        }
    }
}