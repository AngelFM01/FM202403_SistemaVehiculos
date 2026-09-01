using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configs;

public sealed class MarcaConfiguration : IEntityTypeConfiguration<Marca>
{
    public void Configure(EntityTypeBuilder<Marca> builder)
    {
        builder.ToTable("Marcas", "dbo");
        builder.HasKey(x => x.MarcaId);
        builder.Property(x => x.MarcaId).HasColumnName("MarcaID");
        builder.Property(x => x.Nombre).HasColumnName("Nombre").HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Nombre).IsUnique();

        // 1 Marca "Tiene" 1..* Vehiculo
        builder.HasMany(x => x.Vehiculos)
            .WithOne(x => x.Marca)
            .HasForeignKey(x => x.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
