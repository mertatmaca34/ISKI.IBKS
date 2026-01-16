using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ISKI.IBKS.Persistence;

public class IbksDbContextFactory : IDesignTimeDbContextFactory<IbksDbContext>
{
    public IbksDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IbksDbContext>();
        
        // Use SQL Server for migrations (same as production)
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IbksDb;Trusted_Connection=True;");
        
        return new IbksDbContext(optionsBuilder.Options);
    }
}
