using Domain.Models;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class AccessCheckConfiguration: IEntityTypeConfiguration<AccessCheck>
{
    public void Configure(EntityTypeBuilder<AccessCheck> builder)
    {
        builder.ToTable("AccessChecks");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.LicensePlate)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LENGTH_LICENSE_PLATE);

        builder.Property(a => a.CheckDateTime)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(a => a.IsAccessGranted)
            .IsRequired();

        builder.HasOne(a => a.Employee)
            .WithMany()
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}