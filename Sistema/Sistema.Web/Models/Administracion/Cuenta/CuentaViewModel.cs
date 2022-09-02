using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuenta
{
    public class CuentaViewModel
    {
        public int idcuenta { get; set; }
        public int idbanco { get; set; }
        public string banco { get; set; }
        public int idmoneda { get; set; }
        public string moneda { get; set; }
        public string cuenta { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string descripcion { get; set; }

        public bool esempleado { get; set; }
        public bool condicion { get; set; }

        public int idusuario { get; set; }
        public string usuario { get; set; }
    }
}
