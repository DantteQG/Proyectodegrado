using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Especifgasto;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecifgastosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EspecifgastosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Especifgastos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EspecifgastoViewModel>> Listar()
        {
            var especifgasto = await _context.Especifgastos.Include(eg => eg.tipogasto).ToListAsync();
            
            return especifgasto.Select(eg => new EspecifgastoViewModel
            {
                idespecifgasto = eg.idespecifgasto,
                idtipogasto=eg.idtipogasto,
                tipogasto=eg.tipogasto.nombre,
                nombre = eg.nombre,
                descripcion = eg.descripcion,
                dias = eg.dias,
                condicion = eg.condicion
            });
        }

        // GET: api/Especifgastos/Select/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<SelectViewModel>> Select([FromRoute] int id)
        {
            var especifgasto = await _context.Especifgastos.Where(eg => eg.condicion == true).
                Where(eg => eg.idtipogasto == id).ToListAsync();
                

            return especifgasto.Select(eg => new SelectViewModel
            {
                idespecifgasto = eg.idespecifgasto,
                nombre = eg.nombre
            });
        }

        // GET: api/Especifgastos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var especifgasto = await _context.Especifgastos.Include(eg => eg.tipogasto).
                SingleOrDefaultAsync(tg => tg.idespecifgasto==id);

            if (especifgasto == null)
            {
                return NotFound();
            }

            return Ok(new EspecifgastoViewModel
            {
                idespecifgasto = especifgasto.idespecifgasto,
                idtipogasto = especifgasto.idtipogasto,
                tipogasto = especifgasto.tipogasto.nombre,
                nombre = especifgasto.nombre,
                descripcion = especifgasto.descripcion,
                dias = especifgasto.dias,
                condicion = especifgasto.condicion
            });
        }

        // PUT: api/Especifgasto/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idespecifgasto <= 0)
            {
                return BadRequest();
            }

            var especifgasto = await _context.Especifgastos.FirstOrDefaultAsync(eg => eg.idespecifgasto == model.idespecifgasto);


            if (especifgasto == null)
            {
                return NotFound();
            }

            especifgasto.idtipogasto = model.idtipogasto;
            especifgasto.nombre = model.nombre;
            especifgasto.descripcion = model.descripcion;
            especifgasto.dias = model.dias;

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

        // POST: api/Especifgasto/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Especifgasto especifgasto = new Especifgasto
            {
                idtipogasto = model.idtipogasto,
                nombre = model.nombre,
                descripcion = model.descripcion,
                dias = model.dias,
                condicion = true
            };

            _context.Especifgastos.Add(especifgasto);

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

        // DELETE: api/Especifgasto/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var especifgasto = await _context.Especifgastos.FindAsync(id);
            if (especifgasto == null)
            {
                return NotFound();
            }

            _context.Especifgastos.Remove(especifgasto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(especifgasto);
        }

        // PUT: api/Especifgasto/Desactivar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var especifgasto = await _context.Especifgastos.FirstOrDefaultAsync(eg => eg.idespecifgasto == id);


            if (especifgasto == null)
            {
                return NotFound();
            }

            especifgasto.condicion = false;

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

        // PUT: api/Especifgasto/Activar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var especifgasto = await _context.Especifgastos.FirstOrDefaultAsync(eg => eg.idespecifgasto == id);


            if (especifgasto == null)
            {
                return NotFound();
            }

            especifgasto.condicion = true;

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

        private bool EspecifgastoExists(int id)
        {
            return _context.Especifgastos.Any(e => e.idespecifgasto == id);
        }
    }
}
