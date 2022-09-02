using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Cuenta;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CuentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Cuentas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CuentaViewModel>> Listar()
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.banco)
                .Include(c=>c.moneda)
                .Include(c =>c.usuario).ToListAsync();

            return cuenta.Select(c => new CuentaViewModel
            {
                idcuenta = c.idcuenta,
                idbanco = c.idbanco,
                banco = c.banco.nombre,
                idmoneda = c.idmoneda,
                moneda = c.moneda.nombre,
                cuenta = c.cuenta,
                nombre = c.nombre,
                correo = c.correo,
                descripcion = c.descripcion,
                esempleado = c.esempleado,
                condicion = c.condicion,
                idusuario = c.idusuario,
                usuario = c.usuario.nombre
            });
        }

        // GET: api/Cuentas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.banco)
                .Where(c => c.condicion == true).ToListAsync();

            return cuenta.Select(c => new SelectViewModel
            {
                idcuenta = c.idcuenta,
                codigobanco = c.banco.codigobanco,
                banco = c.banco.nombre,
                cuenta = c.cuenta,
                nombre = c.nombre,
            });
        }

        // GET: api/Cuentas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.banco)
                .Include(c =>c.moneda)
                .Include(c =>c.usuario)
                .SingleOrDefaultAsync(c => c.idcuenta == id);

            if (cuenta == null)
            {
                return NotFound();
            }

            return Ok(new CuentaViewModel
            {
                idcuenta = cuenta.idcuenta,
                idbanco=cuenta.idbanco,
                banco = cuenta.banco.nombre,
                idmoneda = cuenta.idmoneda,
                moneda = cuenta.moneda.nombre,
                cuenta = cuenta.cuenta,
                nombre = cuenta.nombre,
                correo = cuenta.correo,
                descripcion = cuenta.descripcion,
                esempleado = cuenta.esempleado,
                condicion = cuenta.condicion,
                idusuario = cuenta.idusuario,
                usuario = cuenta.usuario.nombre
            });
        }

        // PUT: api/Cuentas/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idcuenta <= 0)
            {
                return BadRequest();
            }

            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.idcuenta == model.idcuenta);


            if (cuenta == null)
            {
                return NotFound();
            }

            cuenta.idbanco = model.idbanco;
            cuenta.idmoneda = model.idmoneda;
            cuenta.cuenta = model.cuenta;
            cuenta.nombre = model.nombre;
            cuenta.correo = model.correo;
            cuenta.descripcion = model.descripcion;
            cuenta.esempleado = model.esempleado;

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

        // POST: api/Cuentas/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cuenta cuenta = new Cuenta
            {
                idbanco = model.idbanco,
                idmoneda = model.idmoneda,
                cuenta = model.cuenta,
                nombre = model.nombre,
                correo = model.correo,
                descripcion = model.descripcion,
                esempleado = true,
                condicion = true,
                idusuario = model.idusuario,

            };

            _context.Cuentas.Add(cuenta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();

        }

        // DELETE: api/Cuentas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuenta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(cuenta);
        }

        // PUT: api/Cuentas/Desactivar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.idcuenta == id);


            if (cuenta == null)
            {
                return NotFound();
            }

            cuenta.condicion = false;

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

        // PUT: api/Cuentas/Activar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.idcuenta == id);


            if (cuenta == null)
            {
                return NotFound();
            }

            cuenta.condicion = true;

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
        private bool CuentaExists(int id)
        {
            return _context.Cuentas.Any(e => e.idcuenta == id);
        }
    }
}
