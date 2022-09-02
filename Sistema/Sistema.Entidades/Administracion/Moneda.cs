using Sistema.Entidades.Solicitud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Moneda
    {
        public int idmoneda { get; set; }
        [Required]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        public ICollection<Cuenta> cuenta { get; set; }

        public ICollection<Ordendepago> ordendepago { get; set; }
    }
}
