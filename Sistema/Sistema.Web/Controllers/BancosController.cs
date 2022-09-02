using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Banco;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public BancosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Bancos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<BancoViewModel>> Listar()
        {
            var banco = await _context.Bancos.ToListAsync();
            return banco.Select(b => new BancoViewModel
            {
                idbanco = b.idbanco,
                nombre = b.nombre,
                alias = b.alias,
                descripcion = b.descripcion,
                condicion = b.condicion,
                codigobanco=b.codigobanco
            });
        }

        // GET: api/Bancos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var banco = await _context.Bancos.Where(b => b.condicion == true).ToListAsync();

            return banco.Select(b => new SelectViewModel
            {
                idbanco = b.idbanco,
                nombre = b.nombre,
                alias = b.alias,
                codigobanco = b.codigobanco
            });
        }

        // GET: api/Bancos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var banco = await _context.Bancos.FindAsync(id);

            if (banco == null)
            {
                return NotFound();
            }

            return Ok(new BancoViewModel
            {
                idbanco = banco.idbanco,
                nombre = banco.nombre,
                alias = banco.alias,
                descripcion = banco.descripcion,
                condicion = banco.condicion,
                codigobanco = banco.codigobanco
            });
        }

        // PUT: api/Bancos/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idbanco <= 0)
            {
                return BadRequest();
            }

            var banco = await _context.Bancos.FirstOrDefaultAsync(b => b.idbanco == model.idbanco);


            if (banco == null)
            {
                return NotFound();
            }

            banco.nombre = model.nombre;
            banco.alias = model.alias;
            banco.descripcion = model.descripcion;
            banco.codigobanco = model.codigobanco;

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

        // POST: api/Bancos/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Banco banco = new Banco
            {
                nombre = model.nombre,
                alias = model.alias,
                descripcion = model.descripcion,
                condicion = true,
                codigobanco = model.codigobanco
            };

            _context.Bancos.Add(banco);

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

        // DELETE: api/Bancos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var banco = await _context.Bancos.FindAsync(id);
            if (banco == null)
            {
                return NotFound();
            }

            _context.Bancos.Remove(banco);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(banco);
        }

        //// PUT: api/bancos/Desactivar/1
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var banco = await _context.Bancos.FirstOrDefaultAsync(b => b.idbanco == id);


            if (banco == null)
            {
                return NotFound();
            }

            banco.condicion = false;

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

        //// PUT: api/Bancos/Activar/1
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var banco = await _context.Bancos.FirstOrDefaultAsync(b => b.idbanco == id);


            if (banco == null)
            {
                return NotFound();
            }

            banco.condicion = true;

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



        private bool BancoExists(int id)
        {
            return _context.Bancos.Any(e => e.idbanco == id);
        }
    }
}
