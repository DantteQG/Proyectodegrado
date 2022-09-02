using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Proyecto;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProyectosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Areas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProyectoViewModel>> Listar()
        {
            var proyecto = await _context.Proyectos.ToListAsync();
            return proyecto.Select(p => new ProyectoViewModel
            {
                idproyecto = p.idproyecto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                condicion = p.condicion
            });
        }

        // GET: api/Areas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var proyecto = await _context.Proyectos.Where(p => p.condicion == true).ToListAsync();

            return proyecto.Select(p => new SelectViewModel
            {
                idproyecto = p.idproyecto,
                nombre = p.nombre,

            });
        }

        // GET: api/Areas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return Ok(new ProyectoViewModel
            {
                idproyecto = proyecto.idproyecto,
                nombre = proyecto.nombre,
                descripcion = proyecto.descripcion,
                condicion = proyecto.condicion
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

            if (model.idproyecto <= 0)
            {
                return BadRequest();
            }

            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.idproyecto == model.idproyecto);


            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.nombre = model.nombre;
            proyecto.descripcion = model.descripcion;

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

            Proyecto proyecto = new Proyecto
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Proyectos.Add(proyecto);

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

            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.idproyecto == id);


            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.condicion = false;

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

            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.idproyecto == id);


            if (proyecto == null)
            {
                return NotFound();
            }

            proyecto.condicion = true;

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


        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.idproyecto == id);
        }
    }
}
