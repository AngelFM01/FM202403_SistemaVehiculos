using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("Vehiculos", "dbo");
        builder.HasKey(x => x.VehiculoId);
        builder.Property(x => x.VehiculoId).HasColumnName("VehiculoID");
        builder.Property(x => x.MarcaId).HasColumnName("MarcaID");
        builder.Property(x => x.Modelo).HasColumnName("Modelo").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Anio).HasColumnName("Anio");
        builder.Property(x => x.CantidadPuertas).HasColumnName("CantidadPuertas");
        builder.HasIndex(x => x.MarcaId).HasDatabaseName("IX_Vehiculos_Marca");

        // 1 Vehiculo "es contenido en" 1 Venta
        builder.HasOne(x => x.Venta)
            .WithOne(x => x.Vehiculo)
            .HasForeignKey<Venta>(x => x.VehiculoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
