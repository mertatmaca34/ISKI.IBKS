using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Persistence.Features.Calibrations;

public class CalibrationEntityTypeConfiguration : IEntityTypeConfiguration<Calibration>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Calibration> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.DbColumnName).IsRequired();

        builder.ToTable("Calibrations");
    }
}
