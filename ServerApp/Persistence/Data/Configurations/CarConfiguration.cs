using Domain.Models;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class CarConfiguration: IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.LicensePlate)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LENGTH_LICENSE_PLATE);

        builder.HasIndex(c => c.LicensePlate)
            .IsUnique();

        builder.HasOne(c => c.Employee)
            .WithMany(e => e.Cars)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}