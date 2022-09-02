using Sistema.Entidades.Solicitud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Especifgasto
    {
        public int idespecifgasto { get; set; }
        [Required]
        [ForeignKey("tipogasto")]
        public int idtipogasto { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 100 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public int dias { get; set; }
        public bool condicion { get; set; }


        public Tipogasto tipogasto { get; set; }


        public ICollection<Ordendepago> ordendepago { get; set; }
    }
}
