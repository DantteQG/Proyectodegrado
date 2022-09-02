using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Moneda;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public MonedasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Monedas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MonedaViewModel>> Listar()
        {
            var moneda = await _context.Monedas.ToListAsync();

            return moneda.Select(m => new MonedaViewModel
            {
                idmoneda = m.idmoneda,
                nombre = m.nombre,
                descripcion = m.descripcion,
                condicion = m.condicion
            });
        }

        // GET: api/Monedas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var moneda = await _context.Monedas.Where(m => m.condicion == true).ToListAsync();

            return moneda.Select(m => new SelectViewModel
            {
                idmoneda = m.idmoneda,
                nombre = m.nombre
            });
        }

        private bool MonedaExists(int id)
        {
            return _context.Monedas.Any(e => e.idmoneda == id);
        }
    }
}
