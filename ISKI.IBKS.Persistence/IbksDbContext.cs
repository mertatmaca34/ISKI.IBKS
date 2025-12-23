using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Persistence;

public sealed class IbksDbContext : DbContext
{
    public IbksDbContext(DbContextOptions<IbksDbContext> options) : base(options)
    {
    }
    public DbSet<Calibration> Calibrations => Set<Calibration>();

}
