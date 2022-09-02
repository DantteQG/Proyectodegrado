using Sistema.Web.Models.Solicitud.Detalleorden;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Solicitud.Ordendepago
{
    public class CrearViewModel
    {   
        //PROPIEDADES MAESTRO
        public int idestado { get; set; }
        [Required]
        public int idusuario { get; set; }
        public int idregional { get; set; }
        public int idarea { get; set; }
        public int idempresa { get; set; }
        public int idespecifgasto { get; set; }
        public double dias { get; set; }
        public int idcontador { get; set; }
        public int idaprobador { get; set; }
        public int idmoneda { get; set; }
        public int idtiposolicitud { get; set; }
        public int idproyecto { get; set; }
        public int idmodopago { get; set; }
        public int idcuenta { get; set; }
       // public DateTime fechasolicitud { get; set; }
        //public DateTime fechaprogramada { get; set; }
        public bool factura { get; set; }
        public bool recibo { get; set; }
        public bool rendido { get; set; }
        public string concepto { get; set; }
        public string conceptobanco { get; set; }
        public decimal total { get; set; }


        //PROPIEDADES DETALLE
        [Required]
        public List<DetalleordenViewModel> detalleorden { get; set; }

    }
}
