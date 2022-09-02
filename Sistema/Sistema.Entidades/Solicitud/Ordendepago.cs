using Sistema.Entidades.Administracion;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Solicitud
{
    public class Ordendepago
    {
        public int idordendepago { get; set; }
        [Required]
        [ForeignKey("estado")]
        public int idestado { get; set; }
        [Required]
        [ForeignKey("usuario")]
        public int idusuario { get; set; }
        [Required]
        [ForeignKey("regional")]
        public int idregional { get; set; }
        [Required]
        [ForeignKey("area")]
        public int idarea { get; set; }
        [Required]
        [ForeignKey("empresa")]
        public int idempresa { get; set; }
        [Required]
        [ForeignKey("especifgasto")]
        public int idespecifgasto { get; set; }
        [Required]
        //[ForeignKey("aprobador")]
        public int idcontador { get; set; }
        [Required]
        //[ForeignKey("aprobador")]
        public int idaprobador { get; set; }
        [Required]
        [ForeignKey("moneda")]
        public int idmoneda { get; set; }
        [Required]
        [ForeignKey("tiposolicitud")]
        public int idtiposolicitud { get; set; }
        [Required]
        [ForeignKey("proyecto")]
        public int idproyecto { get; set; }
        [Required]
        [ForeignKey("modopago")]
        public int idmodopago { get; set; }
        [Required]
        [ForeignKey("cuenta")]
        public int idcuenta { get; set; }
        public DateTime fechasolicitud { get; set; }
        public DateTime fechaprogramada { get; set; }
        public bool factura { get; set; }
        public bool recibo { get; set; }
        public bool rendido { get; set; }
        public string concepto { get; set; }
        public string conceptobanco { get; set; }
        public decimal total { get; set; }
        public Estado estado { get; set; }
        [ForeignKey("idusuario")]
        public Usuario usuario { get; set; }
        [ForeignKey("idcontador")]
        public Usuario contador { get; set; }

        [ForeignKey("idaprobador")]
        public Usuario aprobador { get; set; }
        public Regional regional { get; set; }
        public Area area { get; set; }
        public Empresa empresa { get; set; }
        public Especifgasto especifgasto { get; set; }
        public Moneda moneda { get; set; }
        public Tiposolicitud tiposolicitud { get; set; }
        public Proyecto proyecto { get; set; }
        public Modopago modopago { get; set; }
        public Cuenta cuenta { get; set; }

        public ICollection<Detalleorden> detalleorden { get; set; }
        
    }
}
