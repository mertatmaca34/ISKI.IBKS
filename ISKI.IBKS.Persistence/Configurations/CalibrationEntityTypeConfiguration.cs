using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Persistence.Configurations;

public class CalibrationEntityTypeConfiguration : IEntityTypeConfiguration<Calibration>
{
    public void Configure(EntityTypeBuilder<Calibration> builder)
    {
        builder.ToTable("Calibrations");

        var navigation = builder.Metadata.FindNavigation(nameof(Calibration.DbColumnName));
    }
}
