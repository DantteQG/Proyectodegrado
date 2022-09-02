using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Administracion;

namespace Sistema.Datos.Mapping.Administracion
{
    public class RegionalMap : IEntityTypeConfiguration <Regional>
    {
        public void Configure(EntityTypeBuilder <Regional> builder)
        {
            builder.ToTable("regional")
                .HasKey(r => r.idregional);
            builder.Property(r => r.nombre)
                .HasMaxLength(50);
            builder.Property(r => r.descripcion)
                .HasMaxLength(256);

        }
    }
}
