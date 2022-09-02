using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Solicitud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Solicitud
{
    class OrdendepagoMap : IEntityTypeConfiguration<Ordendepago>
    {
        public void Configure(EntityTypeBuilder<Ordendepago> builder)
        {
            builder.ToTable("ordendepago")
                .HasKey(op => op.idordendepago);

            builder.HasOne(op => op.usuario)
                .WithMany(op => op.ordendepago)
                .HasForeignKey(op => op.idusuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(op => op.aprobador)
                .WithMany(op => op.aprobador)
                .HasForeignKey(op => op.idaprobador)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(op => op.contador)
                .WithMany(op => op.contador)
                .HasForeignKey(op => op.idcontador)
                .OnDelete(DeleteBehavior.Cascade);

        }  
    }
}
