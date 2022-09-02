using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Solicitud;
using Sistema.Web.Models.Solicitud.Ordendepago;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdendepagosController : ControllerBase
    {
        private readonly DbContextSistema _context;


        public OrdendepagosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ordendepagos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<OrdendepagoViewModel>> Listar()
        {
            var ordendepago = await _context.Ordendepagos
                .Include(op => op.estado)
                .Include(op => op.usuario)
                .Include(op => op.regional)
                .Include(op => op.area)
                .Include(op => op.empresa)
                .Include(op => op.especifgasto)
                .ThenInclude(ep => ep.tipogasto)
                .Include(op => op.contador)
                .Include(op => op.aprobador)
                .Include(op => op.moneda)
                .Include(op => op.tiposolicitud)
                .Include(op => op.modopago)
                .Include(op =>op.proyecto)
                .Include(op => op.cuenta)
                .ThenInclude(c => c.banco)
                .ToListAsync();

            return ordendepago.Select(op => new OrdendepagoViewModel
            {
                idordendepago = op.idordendepago,
                idestado = op.idestado,
                estado = op.estado.nombre,
                idusuario = op.idusuario,
                usuario = op.usuario.nombre,
                idregional = op.idregional,
                regional = op.regional.nombre,
                idarea = op.idregional,
                area = op.area.nombre,
                idempresa = op.idempresa,
                empresa = op.empresa.nombre,
                idtipogasto = op.especifgasto.idtipogasto,
                nombretipogasto = op.especifgasto.tipogasto.nombre,
                idespecifgasto = op.idespecifgasto,
                especifgasto = op.especifgasto.nombre,
                idcontador = op.idcontador,
                contador = op.contador.nombre,
                idaprobador = op.idaprobador,
                aprobador = op.aprobador.nombre,
                idmoneda = op.idmoneda,
                moneda = op.moneda.nombre,
                idtiposolicitud = op.idtiposolicitud,
                tiposolicitud = op.tiposolicitud.nombre,
                idmodopago = op.idmodopago,
                modopago = op.modopago.nombre,
                idproyecto = op.idproyecto,
                proyecto = op.proyecto.nombre,
                idcuenta = op.idcuenta,
                cuenta = op.cuenta.cuenta + " - " + op.cuenta.nombre + " - " + op.cuenta.banco.alias,
                //nombrecuenta = op.cuenta.nombre,
                idbanco = op.cuenta.banco.idbanco,
                //bancocuenta = op.cuenta.banco.nombre,
                codigobanco = op.cuenta.banco.codigobanco,
                fechasolicitud = op.fechasolicitud.ToString("yyyy-MM-dd"),
                fechaprogramada = op.fechaprogramada.ToString("yyyy-MM-dd"),
                factura = op.factura,
                recibo = op.recibo,
                rendido=op.rendido,
                concepto = op.concepto,
                conceptobanco = op.conceptobanco,
                total = op.total

            });
        }

        // GET: api/Ordendepagos/Missolicitudes/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<OrdendepagoViewModel>> Missolicitudes([FromRoute] int id)
        {
            var ordendepago = await _context.Ordendepagos.Where(op =>op.idusuario == id)
                .Include(op => op.estado)
                .Include(op => op.usuario)
                .Include(op => op.regional)
                .Include(op => op.area)
                .Include(op => op.empresa)
                .Include(op => op.especifgasto)
                .ThenInclude(ep => ep.tipogasto)
                .Include(op => op.contador)
                .Include(op => op.aprobador)
                .Include(op => op.moneda)
                .Include(op => op.tiposolicitud)
                .Include(op => op.modopago)
                .Include(op => op.proyecto)
                .Include(op => op.cuenta)
                .ThenInclude(c => c.banco)
                .ToListAsync();

            return ordendepago.Select(op => new OrdendepagoViewModel
            {
                idordendepago = op.idordendepago,
                idestado = op.idestado,
                estado = op.estado.nombre,
                idusuario = op.idusuario,
                usuario = op.usuario.nombre,
                idregional = op.idregional,
                regional = op.regional.nombre,
                idarea = op.idregional,
                area = op.area.nombre,
                idempresa = op.idempresa,
                empresa = op.empresa.nombre,
                idtipogasto = op.especifgasto.idtipogasto,
                nombretipogasto = op.especifgasto.tipogasto.nombre,
                idespecifgasto = op.idespecifgasto,
                especifgasto = op.especifgasto.nombre,
                idcontador = op.idcontador,
                contador = op.contador.nombre,
                idaprobador = op.idaprobador,
                aprobador = op.aprobador.nombre,
                idmoneda = op.idmoneda,
                moneda = op.moneda.nombre,
                idtiposolicitud = op.idtiposolicitud,
                tiposolicitud = op.tiposolicitud.nombre,
                idmodopago = op.idmodopago,
                modopago = op.modopago.nombre,
                idproyecto = op.idproyecto,
                proyecto = op.proyecto.nombre,
                idcuenta = op.idcuenta,
                cuenta = op.cuenta.cuenta + " - " + op.cuenta.nombre + " - " + op.cuenta.banco.alias,
                //nombrecuenta = op.cuenta.nombre,
                idbanco = op.cuenta.banco.idbanco,
                //bancocuenta = op.cuenta.banco.nombre,
                codigobanco = op.cuenta.banco.codigobanco,
                fechasolicitud = op.fechasolicitud.ToString("yyyy-MM-dd"),
                fechaprogramada = op.fechaprogramada.ToString("yyyy-MM-dd"),
                factura = op.factura,
                recibo = op.recibo,
                rendido = op.rendido,
                concepto = op.concepto,
                conceptobanco = op.conceptobanco,
                total = op.total

            });
        }

        // GET: api/Ordendepagos/poraprobar/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<OrdendepagoViewModel>> Poraprobar([FromRoute] int id)
        {
            var ordendepago = await _context.Ordendepagos
                .Where(op => op.idaprobador == id)
                .Where(op =>op.idestado == 1)
                .Include(op => op.estado)
                .Include(op => op.usuario)
                .Include(op => op.regional)
                .Include(op => op.area)
                .Include(op => op.empresa)
                .Include(op => op.especifgasto)
                .ThenInclude(ep => ep.tipogasto)
                .Include(op => op.contador)
                .Include(op => op.aprobador)
                .Include(op => op.moneda)
                .Include(op => op.tiposolicitud)
                .Include(op => op.modopago)
                .Include(op => op.proyecto)
                .Include(op => op.cuenta)
                .ThenInclude(c => c.banco)
                .ToListAsync();

            return ordendepago.Select(op => new OrdendepagoViewModel
            {
                idordendepago = op.idordendepago,
                idestado = op.idestado,
                estado = op.estado.nombre,
                idusuario = op.idusuario,
                usuario = op.usuario.nombre,
                idregional = op.idregional,
                regional = op.regional.nombre,
                idarea = op.idregional,
                area = op.area.nombre,
                idempresa = op.idempresa,
                empresa = op.empresa.nombre,
                idtipogasto = op.especifgasto.idtipogasto,
                nombretipogasto = op.especifgasto.tipogasto.nombre,
                idespecifgasto = op.idespecifgasto,
                especifgasto = op.especifgasto.nombre,
                idcontador = op.idcontador,
                contador = op.contador.nombre,
                idaprobador = op.idaprobador,
                aprobador = op.aprobador.nombre,
                idmoneda = op.idmoneda,
                moneda = op.moneda.nombre,
                idtiposolicitud = op.idtiposolicitud,
                tiposolicitud = op.tiposolicitud.nombre,
                idmodopago = op.idmodopago,
                modopago = op.modopago.nombre,
                idproyecto = op.idproyecto,
                proyecto = op.proyecto.nombre,
                idcuenta = op.idcuenta,
                cuenta = op.cuenta.cuenta + " - " + op.cuenta.nombre + " - " + op.cuenta.banco.alias,
                //nombrecuenta = op.cuenta.nombre,
                idbanco = op.cuenta.banco.idbanco,
                //bancocuenta = op.cuenta.banco.nombre,
                codigobanco = op.cuenta.banco.codigobanco,
                fechasolicitud = op.fechasolicitud.ToString("yyyy-MM-dd"),
                fechaprogramada = op.fechaprogramada.ToString("yyyy-MM-dd"),
                factura = op.factura,
                recibo = op.recibo,
                rendido = op.rendido,
                concepto = op.concepto,
                conceptobanco = op.conceptobanco,
                total = op.total

            });
        }

        // GET: api/Ordendepagos/EnviadosParticipados/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<OrdendepagoViewModel>> EnviadosParticipados([FromRoute] int id)
        {
            var ordendepago = await _context.Ordendepagos
                .Where(op => op.idaprobador == id || op.idusuario == id)
                .Include(op => op.estado)
                .Include(op => op.usuario)
                .Include(op => op.regional)
                .Include(op => op.area)
                .Include(op => op.empresa)
                .Include(op => op.especifgasto)
                .ThenInclude(ep => ep.tipogasto)
                .Include(op => op.contador)
                .Include(op => op.aprobador)
                .Include(op => op.moneda)
                .Include(op => op.tiposolicitud)
                .Include(op => op.modopago)
                .Include(op => op.proyecto)
                .Include(op => op.cuenta)
                .ThenInclude(c => c.banco)
                .ToListAsync();

            return ordendepago.Select(op => new OrdendepagoViewModel
            {
                idordendepago = op.idordendepago,
                idestado = op.idestado,
                estado = op.estado.nombre,
                idusuario = op.idusuario,
                usuario = op.usuario.nombre,
                idregional = op.idregional,
                regional = op.regional.nombre,
                idarea = op.idregional,
                area = op.area.nombre,
                idempresa = op.idempresa,
                empresa = op.empresa.nombre,
                idtipogasto = op.especifgasto.idtipogasto,
                nombretipogasto = op.especifgasto.tipogasto.nombre,
                idespecifgasto = op.idespecifgasto,
                especifgasto = op.especifgasto.nombre,
                idcontador = op.idcontador,
                contador = op.contador.nombre,
                idaprobador = op.idaprobador,
                aprobador = op.aprobador.nombre,
                idmoneda = op.idmoneda,
                moneda = op.moneda.nombre,
                idtiposolicitud = op.idtiposolicitud,
                tiposolicitud = op.tiposolicitud.nombre,
                idmodopago = op.idmodopago,
                modopago = op.modopago.nombre,
                idproyecto = op.idproyecto,
                proyecto = op.proyecto.nombre,
                idcuenta = op.idcuenta,
                cuenta = op.cuenta.cuenta + " - " + op.cuenta.nombre + " - " + op.cuenta.banco.alias,
                //nombrecuenta = op.cuenta.nombre,
                idbanco = op.cuenta.banco.idbanco,
                //bancocuenta = op.cuenta.banco.nombre,
                codigobanco = op.cuenta.banco.codigobanco,
                fechasolicitud = op.fechasolicitud.ToString("yyyy-MM-dd"),
                fechaprogramada = op.fechaprogramada.ToString("yyyy-MM-dd"),
                factura = op.factura,
                recibo = op.recibo,
                rendido = op.rendido,
                concepto = op.concepto,
                conceptobanco = op.conceptobanco,
                total = op.total

            });
        }

        // GET: api/Ordendepagos/EnviadosParticipados/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<OrdendepagoViewModel>> Porestado([FromRoute] int id)
        {
            var ordendepago = await _context.Ordendepagos
                .Where(op => op.idestado == id)
                .Include(op => op.estado)
                .Include(op => op.usuario)
                .Include(op => op.regional)
                .Include(op => op.area)
                .Include(op => op.empresa)
                .Include(op => op.especifgasto)
                .ThenInclude(ep => ep.tipogasto)
                .Include(op => op.contador)
                .Include(op => op.aprobador)
                .Include(op => op.moneda)
                .Include(op => op.tiposolicitud)
                .Include(op => op.modopago)
                .Include(op => op.proyecto)
                .Include(op => op.cuenta)
                .ThenInclude(c => c.banco)
                .ToListAsync();

            return ordendepago.Select(op => new OrdendepagoViewModel
            {
                idordendepago = op.idordendepago,
                idestado = op.idestado,
                estado = op.estado.nombre,
                idusuario = op.idusuario,
                usuario = op.usuario.nombre,
                idregional = op.idregional,
                regional = op.regional.nombre,
                idarea = op.idregional,
                area = op.area.nombre,
                idempresa = op.idempresa,
                empresa = op.empresa.nombre,
                idtipogasto = op.especifgasto.idtipogasto,
                nombretipogasto = op.especifgasto.tipogasto.nombre,
                idespecifgasto = op.idespecifgasto,
                especifgasto = op.especifgasto.nombre,
                idcontador = op.idcontador,
                contador = op.contador.nombre,
                idaprobador = op.idaprobador,
                aprobador = op.aprobador.nombre,
                idmoneda = op.idmoneda,
                moneda = op.moneda.nombre,
                idtiposolicitud = op.idtiposolicitud,
                tiposolicitud = op.tiposolicitud.nombre,
                idmodopago = op.idmodopago,
                modopago = op.modopago.nombre,
                idproyecto = op.idproyecto,
                proyecto = op.proyecto.nombre,
                idcuenta = op.idcuenta,
                cuenta = op.cuenta.cuenta + " - " + op.cuenta.nombre + " - " + op.cuenta.banco.alias,
                //nombrecuenta = op.cuenta.nombre,
                idbanco = op.cuenta.banco.idbanco,
                //bancocuenta = op.cuenta.banco.nombre,
                codigobanco = op.cuenta.banco.codigobanco,
                fechasolicitud = op.fechasolicitud.ToString("yyyy-MM-dd"),
                fechaprogramada = op.fechaprogramada.ToString("yyyy-MM-dd"),
                factura = op.factura,
                recibo = op.recibo,
                rendido = op.rendido,
                concepto = op.concepto,
                conceptobanco = op.conceptobanco,
                total = op.total

            });
        }

        // POST: api/Ordendepagos/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fechasol = DateTime.Now;
            DateTime fechaprog = DateTime.Now;
            double dias;
            dias = model.dias;

            fechaprog = fechaprog.AddDays(dias * 1);


            Ordendepago ordendepago = new Ordendepago
            {
                idestado = 1,
                idusuario = model.idusuario,
                idregional = model.idregional,
                idarea = model.idarea,
                idempresa = model.idempresa,
                idespecifgasto = model.idespecifgasto,
                idcontador = model.idcontador,
                idaprobador = model.idaprobador,
                idmoneda = model.idmoneda,
                idtiposolicitud = model.idtiposolicitud,
                idproyecto = model.idproyecto,
                idmodopago = model.idmodopago,
                idcuenta = model.idcuenta,
                fechasolicitud = fechasol,
                fechaprogramada = fechaprog,
                factura = model.factura,
                recibo = model.recibo,
                rendido = false,
                concepto = model.concepto,
                conceptobanco = model.concepto,
                total = model.total
            };



            try
            {
                _context.Ordendepagos.Add(ordendepago);
                await _context.SaveChangesAsync();
                var id = ordendepago.idordendepago;
                foreach (var det in model.detalleorden)
                {
                    Detalleorden detalleorden = new Detalleorden
                    {
                        idordendepago = id,
                        detalle = det.detalle,
                        nrodocumento = det.nrodocumento,
                        monto = det.monto,

                    };
                    _context.Detalleordenes.Add(detalleorden);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            
            //return Ok();
            return Ok(new { id = ordendepago.idordendepago});

        }

        // PUT: api/Ordendepagos/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idordendepago <= 0)
            {
                return BadRequest();
            }


            _context.Detalleordenes.RemoveRange(_context.Detalleordenes.Where(dor => dor.idordendepago == model.idordendepago));

            await _context.SaveChangesAsync();

            var ordendepago = await _context.Ordendepagos.FirstOrDefaultAsync(op => op.idordendepago == model.idordendepago);


            if (ordendepago == null)
            {
                return NotFound();
            }

            ordendepago.idestado = model.idestado;
            ordendepago.idusuario = model.idusuario;
            ordendepago.idregional = model.idregional;
            ordendepago.idarea = model.idarea;
            ordendepago.idempresa = model.idempresa;
            ordendepago.idespecifgasto = model.idespecifgasto;
            ordendepago.idcontador = model.idcontador;
            //ordendepago.idaprobador = model.idaprobador;
            ordendepago.idmoneda = model.idmoneda;
            ordendepago.idtiposolicitud = model.idtiposolicitud;
            ordendepago.idmodopago = model.idmodopago;
            ordendepago.idproyecto = model.idproyecto;
            ordendepago.idcuenta = model.idcuenta;
            ordendepago.fechaprogramada = model.fechaprogramada;
            ordendepago.factura = model.factura;
            ordendepago.recibo = model.recibo;
            ordendepago.concepto = model.concepto;
            ordendepago.conceptobanco = model.concepto;
            ordendepago.total = model.total;
            //ordendepago.detalleorden = model.detalleorden;

            try
            {
                await _context.SaveChangesAsync();
                var id = model.idordendepago;
                foreach (var det in model.detalleorden)
                {
                    Detalleorden detalleorden = new Detalleorden
                    {
                        idordendepago = id,
                        detalle = det.detalle,
                        nrodocumento = det.nrodocumento,
                        monto = det.monto,

                    };
                    _context.Detalleordenes.Add(detalleorden);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Ordendepagos/Rechazar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Rechazar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var ordendepago = await _context.Ordendepagos.FirstOrDefaultAsync(op => op.idordendepago == id);

            if (ordendepago == null)
            {
                return NotFound();
            }

            ordendepago.idestado = 5;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Ordendepagos/Cambiaraprobador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Cambiaraprobador([FromBody] ActAprobadorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idordendepago <= 0)
            {
                return BadRequest();
            }

            var ordendepago = await _context.Ordendepagos.FirstOrDefaultAsync(op => op.idordendepago == model.idordendepago);

            if (ordendepago == null)
            {
                return NotFound();
            }

            ordendepago.idaprobador = model.idaprobador;
 
            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }


        private bool OrdendepagoExists(int id)
        {
            return _context.Ordendepagos.Any(e => e.idordendepago == id);
        }
    }
}
