using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Modopago;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModopagosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ModopagosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Modopagos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ModopagoViewModel>> Listar()
        {
            var modopago = await _context.Modopagos.ToListAsync();

            return modopago.Select(mp => new ModopagoViewModel
            {
                idmodopago = mp.idmodopago,
                nombre = mp.nombre,
                descripcion = mp.descripcion,
                condicion = mp.condicion
            });
        }

        // GET: api/Empresas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var modopago = await _context.Modopagos.Where(mp => mp.condicion == true).ToListAsync();

            return modopago.Select(mp => new SelectViewModel
            {
                idmodopago = mp.idmodopago,
                nombre = mp.nombre
            });
        }

        private bool ModopagoExists(int id)
        {
            return _context.Modopagos.Any(e => e.idmodopago == id);
        }
    }
}
