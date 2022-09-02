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
    class EspecifgastoMap : IEntityTypeConfiguration<Especifgasto>
    {
        public void Configure(EntityTypeBuilder<Especifgasto> builder)
        {
            builder.ToTable("especifgasto")
                .HasKey(eg => eg.idespecifgasto);
            

        }
    }
}
