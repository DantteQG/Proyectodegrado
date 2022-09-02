using Sistema.Entidades.Solicitud;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Cuenta
    {
        public int idcuenta { get; set; }
        
        [Required]
        [ForeignKey("banco")]
        public int idbanco { get; set; }
        
        [ForeignKey("moneda")]
        public int idmoneda { get; set; }
        
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "la cuenta no debe contener mas de 20 caracteres, ni menos de 3 caracteres y debe ir sin guiones")]
        public string cuenta { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string correo { get; set; }

        public bool esempleado { get; set; }
        public bool condicion { get; set; }

        [Required]
        [ForeignKey("usuario")]
        public int idusuario { get; set; }


        public Banco banco { get; set; }
        public Moneda moneda { get; set; }
        public Usuario usuario { get; set; }



        public ICollection<Ordendepago> ordendepago { get; set; }
    }

}
