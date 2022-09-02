using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuenta
{
    public class SelectViewModel
    {
        public int idcuenta { get; set; }
        public string banco { get; set; }
        public int codigobanco { get; set; }
        public string cuenta { get; set; }
        public string nombre { get; set; }


    }
}
