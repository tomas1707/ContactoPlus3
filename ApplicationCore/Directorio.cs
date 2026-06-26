using ContactosPlus.Domain;
using ContactosPlus.Infrastructure;

namespace ContactosPlus.ApplicationCore
{
    internal class Directorio
    {
        private List<Contacto> midirectorio;
        public Directorio()
        {
            midirectorio = new List<Contacto>();
        }

        public bool Add(string nombre, string apellidos, string telefono, string extension, string puesto, string empresa, string correo, string observaciones)
        {
            if (Existe(nombre, apellidos))
                return false;

            Contacto contacto = new Contacto(
                NextId(),
                nombre,
                apellidos,
                telefono,
                extension,
                puesto,
                empresa,
                correo,
                observaciones
            );

            return Add(contacto);
        }

        public bool Add(Contacto contacto)
        {
            if (Existe(contacto.nombre, contacto.apellidos))
                return false;

            midirectorio.Add(contacto);

            return true;
        }

        public bool Existe(string nombre, string apellidos)
        {
            foreach (Contacto c in midirectorio)
            {
                bool mismoNombre =
                    c.nombre.Trim().Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase);

                bool mismosApellidos =
                    c.apellidos.Trim().Equals(apellidos.Trim(), StringComparison.OrdinalIgnoreCase);

                if (mismoNombre && mismosApellidos)
                    return true;
            }

            return false;
        }

        public List<Contacto> Show()
        {
            return midirectorio;
        }

        public Contacto? Find(int id)
        {
            foreach (Contacto contacto in midirectorio)
            {
                if (contacto.id == id)
                    return contacto;
            }

            return null;
        }

        public bool Update(Contacto datos)
        {
            Contacto? actual = Find(datos.id);

            if (actual == null)
                return false;

            actual.nombre = datos.nombre;
            actual.apellidos = datos.apellidos;
            actual.telefono = datos.telefono;
            actual.extension = datos.extension;
            actual.puesto = datos.puesto;
            actual.empresa = datos.empresa;
            actual.correo = datos.correo;
            actual.observaciones = datos.observaciones;
            actual.estatus = datos.estatus;

            return true;
        }

        public bool Delete(int id)
        {
            Contacto? contacto = Find(id);

            if (contacto == null)
                return false;

            midirectorio.Remove(contacto);

            return true;
        }

        public int NextId()
        {
            if (midirectorio.Count == 0)
                return 1;

            return midirectorio.Max(c => c.id) + 1;
        }

        public void Clear()
        {
            midirectorio.Clear();
        }

        public int GenerarId()
        {
            if (midirectorio.Count == 0)
                return 1;

            int mayor = midirectorio[0].id;

            foreach (Contacto contacto in midirectorio)
            {
                if (contacto.id > mayor)
                {
                    mayor = contacto.id;
                }
            }

            return mayor + 1;
        }
    }
}