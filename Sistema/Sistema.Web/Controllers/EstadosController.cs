using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Estado;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EstadosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Estados/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EstadoViewModel>> Listar()
        {
            var estado = await _context.Estados.ToListAsync();

            return estado.Select(e => new EstadoViewModel
            {
                idestado = e.idestado,
                nombre = e.nombre,
                descripcion = e.descripcion,
                condicion = e.condicion
            });
        }

        // GET: api/Empresas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var estado = await _context.Estados.Where(e => e.condicion == true).ToListAsync();

            return estado.Select(e => new SelectViewModel
            {
                idestado = e.idestado,
                nombre = e.nombre
            });
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.idestado == id);
        }
    }
}
