using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Solicitud
{
    public class Detalleorden
    {
        public int iddetalleorden { get; set; }
        [ForeignKey("ordendepago")]
        [Required]
        public int idordendepago { get; set; }
        public string detalle { get; set; }
        public string nrodocumento { get; set; }
        public decimal monto { get; set; }
        
        public Ordendepago ordendepago { get; set; }
    }
}
