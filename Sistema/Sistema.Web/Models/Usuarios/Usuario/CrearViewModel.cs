using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class CrearViewModel
    {
        
        [Required]
        public int idrol { get; set; }
        public string nombre { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string telefono { get; set; }
        [Required]
        public string usuario { get; set; }
        [Required]
        public string password { get; set; }
    
        public string descripcion { get; set; }
    }
}
