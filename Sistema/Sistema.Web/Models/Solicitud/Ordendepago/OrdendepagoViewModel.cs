using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Solicitud.Ordendepago
{
    public class OrdendepagoViewModel
    {
        public int idordendepago { get; set; }
        public int idestado { get; set; }
        public string estado { get; set; }
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public int idregional { get; set; }
        public string regional { get; set; }
        public int idarea { get; set; }
        public string area { get; set; }
        public int idempresa { get; set; }
        public string empresa { get; set; }
        public int idtipogasto { get; set; }
        public string nombretipogasto { get; set; }
        public int idespecifgasto { get; set; }
        public string especifgasto { get; set; }
        public double dias { get; set; }
        public int idcontador { get; set; }
        public string contador { get; set; }
        public int idaprobador { get; set; }
        public string aprobador { get; set; }
        public int idmoneda { get; set; }
        public string moneda { get; set; }
        public int idtiposolicitud { get; set; }
        public string tiposolicitud { get; set; }
        public int idmodopago { get; set; }
        public string modopago { get; set; }
        public int idproyecto { get; set; }
        public string proyecto { get; set; }
        public int idcuenta { get; set; }
        public string cuenta { get; set; }
        public string nombrecuenta { get; set; }
        public int idbanco { get; set; }
        public string bancocuenta { get; set; }
        public int codigobanco { get; set; }
        public string fechasolicitud { get; set; }
        public string fechaprogramada { get; set; }
        public bool factura { get; set; }
        public bool recibo { get; set; }
        public bool rendido { get; set; }
        public string concepto { get; set; }
        public string conceptobanco { get; set; }
        public decimal total { get; set; }

    }

    
}
