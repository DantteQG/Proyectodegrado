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
    public class DetalleordenMap : IEntityTypeConfiguration<Detalleorden>
    {
        public void Configure(EntityTypeBuilder<Detalleorden> builder)
        {
            builder.ToTable("detalleorden")
                .HasKey(d => d.iddetalleorden);

           
        }  
    

    }
}
