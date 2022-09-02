using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Banco
{
    public class BancoViewModel
    {
        public int idbanco { get; set; }
        public string nombre { get; set; }
        public string alias { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public int codigobanco { get; set; }
    }
}
