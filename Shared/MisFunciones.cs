using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactosPlus.Shared
{
    internal class MisFunciones
    {

        //  el modificador de acceso static se utiliza para invocar un metodo
        // sin necesidad de instancias la clase
        public static void mensajeAlerta(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
