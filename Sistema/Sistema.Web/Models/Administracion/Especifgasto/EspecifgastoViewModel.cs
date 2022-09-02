using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Especifgasto
{
    public class EspecifgastoViewModel
    {
        public int idespecifgasto { get; set; }
        public int idtipogasto { get; set; }
        public string tipogasto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public int dias { get; set; }
        public bool condicion { get; set; }

    }
}
