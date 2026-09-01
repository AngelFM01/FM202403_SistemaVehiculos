using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Ventas", "dbo");
        builder.HasKey(x => x.VentaId);
        builder.Property(x => x.VentaId).HasColumnName("VentaID");
        builder.Property(x => x.VehiculoId).HasColumnName("VehiculoID");
        builder.Property(x => x.TotalVenta).HasColumnName("TotalVenta").HasColumnType("float");
        builder.Property(x => x.Cantidad).HasColumnName("Cantidad");
        builder.HasIndex(x => x.VehiculoId).IsUnique().HasDatabaseName("IX_Ventas_Vehiculo");
    }
}
