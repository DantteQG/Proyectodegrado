using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuenta
{
    public class ActualizarViewModel
    {
        [Required]
        public int idcuenta { get; set; }
        [Required]
        public int idbanco { get; set; }
        [Required]
        public int idmoneda { get; set; }

        [Required]
        public string cuenta { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 50 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }

        public string correo { get; set; }
        public string descripcion { get; set; }
        public bool esempleado { get; set; }

        [Required]
        public int idusuario { get; set; }

    }
}
