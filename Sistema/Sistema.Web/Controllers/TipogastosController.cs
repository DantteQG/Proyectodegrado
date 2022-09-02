using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Tipogasto;


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipogastosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public TipogastosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Tipogastos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipogastoViewModel>> Listar()
        {
            var tipogasto = await _context.Tipogastos.ToListAsync();
            return tipogasto.Select(tg => new TipogastoViewModel
            {
                idtipogasto = tg.idtipogasto,
                nombre = tg.nombre,
                descripcion = tg.descripcion,
                condicion = tg.condicion
            });
        }

        // GET: api/Tipogastos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var tipogasto = await _context.Tipogastos.Where(tg =>tg.condicion==true).ToListAsync();
           
            return tipogasto.Select(tg => new SelectViewModel
            {
                idtipogasto = tg.idtipogasto,
                nombre = tg.nombre
            });
        }

        // GET: api/Tipogastos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var tipogasto = await _context.Tipogastos.FindAsync(id);

            if (tipogasto == null)
            {
                return NotFound();
            }

            return Ok(new TipogastoViewModel
            {
                idtipogasto = tipogasto.idtipogasto,
                nombre = tipogasto.nombre,
                descripcion = tipogasto.descripcion,
                condicion = tipogasto.condicion
            });
        }

        // PUT: api/Tipogastos/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idtipogasto <= 0)
            {
                return BadRequest();
            }

            var tipogasto = await _context.Tipogastos.FirstOrDefaultAsync(tg => tg.idtipogasto == model.idtipogasto);


            if (tipogasto == null)
            {
                return NotFound();
            }

            tipogasto.nombre = model.nombre;
            tipogasto.descripcion = model.descripcion;

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

        // POST: api/Tipogastos/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tipogasto tipogasto = new Tipogasto
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Tipogastos.Add(tipogasto);

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

        // DELETE: api/Tipogastos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var tipogasto = await _context.Tipogastos.FindAsync(id);
            if (tipogasto == null)
            {
                return NotFound();
            }

            _context.Tipogastos.Remove(tipogasto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(tipogasto);
        }

        //// PUT: api/Tipogastos/Desactivar/1
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var tipogasto = await _context.Tipogastos.FirstOrDefaultAsync(tg => tg.idtipogasto == id);


            if (tipogasto == null)
            {
                return NotFound();
            }

            tipogasto.condicion = false;

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

        //// PUT: api/Tipogastos/Activar/1
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var tipogasto = await _context.Tipogastos.FirstOrDefaultAsync(tg => tg.idtipogasto == id);


            if (tipogasto == null)
            {
                return NotFound();
            }

            tipogasto.condicion = true;

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


        private bool TipogastoExists(int id)
        {
            return _context.Tipogastos.Any(e => e.idtipogasto == id);
        }
    }
}
