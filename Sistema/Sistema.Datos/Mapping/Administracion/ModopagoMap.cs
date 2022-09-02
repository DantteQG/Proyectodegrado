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
    class ModopagoMap : IEntityTypeConfiguration<Modopago>
    {
        public void Configure(EntityTypeBuilder<Modopago> builder)
        {
            builder.ToTable("modopago")
                .HasKey(mp => mp.idmodopago);

        }
    }

}
