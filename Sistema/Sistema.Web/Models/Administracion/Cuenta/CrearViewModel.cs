using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuenta
{
    public class CrearViewModel
    {

        [Required]
        public int idbanco { get; set; }
        [Required]
        public int idmoneda { get; set; }
        [Required]
        public string cuenta { get; set; }

        [Required]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string correo { get; set; }
        public bool esempleado { get; set; }

        [Required]
        public int idusuario { get; set; }
    }
}
