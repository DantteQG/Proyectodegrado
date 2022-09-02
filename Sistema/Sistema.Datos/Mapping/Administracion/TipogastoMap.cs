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
    public class TipogastoMap : IEntityTypeConfiguration<Tipogasto>
    {
        public void Configure(EntityTypeBuilder<Tipogasto> builder)
        {
            builder.ToTable("tipogasto")
                .HasKey(tg => tg.idtipogasto);
            builder.Property(tg => tg.nombre)
                .HasMaxLength(50);
            builder.Property(tg => tg.descripcion)
                .HasMaxLength(256);

        }
    } 
}
