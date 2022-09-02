using Sistema.Entidades.Administracion;
using Sistema.Entidades.Solicitud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Usuarios
{
    public class Usuario
    {
        public int idusuario { get; set; }
        [Required]
        [ForeignKey("rol")]
        public int idrol { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        [Required]
        public string usuario { get; set; }
        [Required]
        public byte[] password_hash{ get; set; }
        [Required]
        public byte[] password_salt { get; set; }

        public string descripcion { get; set; }
        public bool condicion { get; set; }



        public Rol rol { get; set; }
        public ICollection<Ordendepago> ordendepago { get; set; }

        [InverseProperty(nameof(Ordendepago.aprobador))]
        public ICollection<Ordendepago> aprobador { get; set; }
        [InverseProperty(nameof(Ordendepago.contador))]
        public ICollection<Ordendepago> contador { get; set; }
        public ICollection<Cuenta> cuenta { get; set; }


    }
}
