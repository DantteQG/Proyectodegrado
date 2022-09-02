using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Administracion;
using Sistema.Datos.Mapping.Solicitud;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Administracion;
using Sistema.Entidades.Solicitud;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Regional> Regionales { get; set; } 
        public DbSet<Tipogasto> Tipogastos { get; set; }
        public DbSet<Especifgasto> Especifgastos { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Banco> Bancos{ get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Modopago> Modopagos { get; set; }
        public DbSet<Tiposolicitud> Tiposolicitudes { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Ordendepago> Ordendepagos { get; set; }
        public DbSet<Detalleorden> Detalleordenes { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RegionalMap());
            modelBuilder.ApplyConfiguration(new TipogastoMap());
            modelBuilder.ApplyConfiguration(new EspecifgastoMap());
            modelBuilder.ApplyConfiguration(new MonedaMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new AreaMap());
            modelBuilder.ApplyConfiguration(new BancoMap());
            modelBuilder.ApplyConfiguration(new CuentaMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new ProyectoMap());
            modelBuilder.ApplyConfiguration(new ModopagoMap());
            modelBuilder.ApplyConfiguration(new TiposolicitudMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new OrdendepagoMap());
            modelBuilder.ApplyConfiguration(new DetalleordenMap());
        }




    }
}
