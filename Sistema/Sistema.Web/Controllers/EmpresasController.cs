using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Empresa;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EmpresasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Empresas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EmpresaViewModel>> Listar()
        {
            var empresa = await _context.Empresas.ToListAsync();

            return empresa.Select(e => new EmpresaViewModel
            {
                idempresa = e.idempresa,
                nombre = e.nombre,
                descripcion = e.descripcion,
                condicion = e.condicion
            });
        }

        // GET: api/Empresas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var empresa = await _context.Empresas.Where(e => e.condicion == true).ToListAsync();

            return empresa.Select(e => new SelectViewModel
            {
                idempresa = e.idempresa,
                nombre = e.nombre
            });
        }
        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.idempresa == id);
        }
    }
}
