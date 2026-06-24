using ContactosPlus.Domain;

namespace ContactosPlus.ApplicationCore
{
    internal class Directorio
    {
        private List<Contacto> midirectorio = new List<Contacto>();

        public void add(int id, string nombre, string apellidos, int telefono, int extension, string puesto, string empresa, string correo, string observaciones)
        {
            //Agrega multioples contactos
            midirectorio.Add(new Contacto(id, nombre, apellidos, telefono, extension, puesto, empresa, correo,observaciones));

        }

        public void show()
        {
            foreach (Contacto dato in midirectorio)
            {
                Console.WriteLine(dato);
            }
        }

    }
}
