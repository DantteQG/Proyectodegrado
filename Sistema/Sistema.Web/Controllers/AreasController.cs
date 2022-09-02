using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Area;

namespace Sistema.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public AreasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Areas/Listar
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<AreaViewModel>> Listar()
        {
            var area = await _context.Areas.ToListAsync();
            return area.Select(a => new AreaViewModel
            {
                idarea = a.idarea,
                nombre = a.nombre,
                descripcion = a.descripcion,
                condicion = a.condicion
            });
        }

        // GET: api/Areas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var area = await _context.Areas.Where(a => a.condicion == true).ToListAsync();

            return area.Select(a => new SelectViewModel
            {
                idarea = a.idarea,
                nombre = a.nombre,
 
            });
        }

        // GET: api/Areas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return Ok(new AreaViewModel
            {
                idarea = area.idarea,
                nombre = area.nombre,
                descripcion = area.descripcion,
                condicion = area.condicion
            });
        }

        // PUT: api/Areas/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idarea <= 0)
            {
                return BadRequest();
            }

            var area = await _context.Areas.FirstOrDefaultAsync(a => a.idarea == model.idarea);


            if (area == null)
            {
                return NotFound();
            }

            area.nombre = model.nombre;
            area.descripcion = model.descripcion;

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

        // POST: api/Areas/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Area area = new Area
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Areas.Add(area);

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

        // DELETE: api/Areas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Areas.Remove(area);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(area);
        }

        // PUT: api/Areas/Desactivar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await _context.Areas.FirstOrDefaultAsync(a => a.idarea == id);


            if (area == null)
            {
                return NotFound();
            }

            area.condicion = false;

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

        // PUT: api/Areas/Activar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await _context.Areas.FirstOrDefaultAsync(a => a.idarea == id);


            if (area == null)
            {
                return NotFound();
            }

            area.condicion = true;

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

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.idarea == id);
        }
    }
}
