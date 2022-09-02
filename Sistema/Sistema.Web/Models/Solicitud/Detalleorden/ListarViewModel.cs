using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Solicitud.Detalleorden
{
    public class ListarViewModel
    {
        public int idordendepago { get; set; }
        public string detalle { get; set; }
        public string nrodocumento { get; set; }
        public decimal monto { get; set; }
    }
}
