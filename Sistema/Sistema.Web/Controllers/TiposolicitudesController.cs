using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Tiposolicitud;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposolicitudesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public TiposolicitudesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Tiposolicitudes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TiposolicitudViewModel>> Listar()
        {
            var tiposolicitud = await _context.Tiposolicitudes.ToListAsync();

            return tiposolicitud.Select(ts => new TiposolicitudViewModel
            {
                idtiposolicitud = ts.idtiposolicitud,
                nombre = ts.nombre,
                descripcion = ts.descripcion,
                condicion = ts.condicion
            });
        }

        // GET: api/Tiposolicitudes/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var tiposolicitud = await _context.Tiposolicitudes.Where(e => e.condicion == true).ToListAsync();

            return tiposolicitud.Select(ts => new SelectViewModel
            {
                idtiposolicitud = ts.idtiposolicitud,
                nombre = ts.nombre
            });
        }

        private bool TiposolicitudExists(int id)
        {
            return _context.Tiposolicitudes.Any(e => e.idtiposolicitud == id);
        }
    }
}
