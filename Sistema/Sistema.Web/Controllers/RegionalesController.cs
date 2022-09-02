using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Regional;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionalesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RegionalesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Regionales/Listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<RegionalViewModel>> Listar()
        {
            var regional = await _context.Regionales.ToListAsync();
            return regional.Select(r => new RegionalViewModel
            {
                idregional=r.idregional,
                nombre=r.nombre,
                descripcion=r.descripcion,
                condicion=r.condicion
            });
        }


        // GET: api/Regionales/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var regional = await _context.Regionales.Where(r => r.condicion == true).ToListAsync();

            return regional.Select(r => new SelectViewModel
            {
                idregional = r.idregional,
                nombre = r.nombre
            });
        }

        // GET: api/Regionales/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var regional = await _context.Regionales.FindAsync(id);

            if (regional == null)
            {
                return NotFound();
            }

            return Ok(new RegionalViewModel
            {
                idregional = regional.idregional,
                nombre = regional.nombre,
                descripcion = regional.descripcion,
                condicion = regional.condicion
            });
        }

        // PUT: api/Regionales/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (model.idregional <= 0)
            {
                return BadRequest();
            }

            var regional = await _context.Regionales.FirstOrDefaultAsync(r => r.idregional == model.idregional);


            if(regional==null)
            {
                return NotFound();
            }

            regional.nombre = model.nombre;
            regional.descripcion = model.descripcion;

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

        // POST: api/Regionales/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Regional regional = new Regional
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Regionales.Add(regional);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok();

         }

        // DELETE: api/Regionales/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute]int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var regional = await _context.Regionales.FindAsync(id);
            if (regional == null)
            {
                return NotFound();
            }

            _context.Regionales.Remove(regional);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok(regional);
        }

        // PUT: api/Regionales/Desactivar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
           

            if (id <= 0)
            {
                return BadRequest();
            }

            var regional = await _context.Regionales.FirstOrDefaultAsync(r => r.idregional == id);


            if (regional == null)
            {
                return NotFound();
            }

            regional.condicion = false;

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

        // PUT: api/Regionales/Activar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var regional = await _context.Regionales.FirstOrDefaultAsync(r => r.idregional == id);


            if (regional == null)
            {
                return NotFound();
            }

            regional.condicion = true;

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

        private bool RegionalExists(int id)
        {
            return _context.Regionales.Any(e => e.idregional == id);
        }
    }
}
