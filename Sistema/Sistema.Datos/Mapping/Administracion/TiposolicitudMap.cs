using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Administracion
{
    class TiposolicitudMap : IEntityTypeConfiguration<Tiposolicitud>
    {
        public void Configure(EntityTypeBuilder<Tiposolicitud> builder)
        {
            builder.ToTable("tiposolicitud")
                .HasKey(ts => ts.idtiposolicitud);


        }
    }
}
