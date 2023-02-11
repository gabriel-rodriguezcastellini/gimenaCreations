using GimenaCreations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GimenaCreations.EntityConfigurations;

public class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");
        builder.HasKey(t => t.Id);
        builder.Property(x=>x.Website).IsRequired();
        builder.Property(x=>x.Cuit).IsRequired();
        builder.Property(x=>x.AfipResponsibility).IsRequired();
        builder.Property(x=>x.CompanyAddress).IsRequired();
        builder.Property(x=>x.Email).IsRequired();
        builder.Property(x=>x.Phone).IsRequired();
        builder.Property(x=>x.Name).IsRequired();
    }
}
