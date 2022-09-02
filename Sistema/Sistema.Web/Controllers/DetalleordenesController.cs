using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Solicitud;
using Sistema.Web.Models.Solicitud.Detalleorden;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleordenesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public DetalleordenesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Detelleordenes/Listar/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarViewModel>> Listar([FromRoute] int id)
        {
            var detalleorden = await _context.Detalleordenes
                .Where(dor => dor.idordendepago == id).ToListAsync();


            return detalleorden.Select(dor => new ListarViewModel
            {
                detalle = dor.detalle,
                nrodocumento = dor.nrodocumento,
                monto = dor.monto
                
            });
        }
        private bool DetalleordenExists(int id)
        {
            return _context.Detalleordenes.Any(e => e.iddetalleorden == id);
        }
    }
}
