using Microsoft.EntityFrameworkCore;

namespace AccountsReceivable.BAL.Data;

public class ApplicationContextDesignFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
{
    public ApplicationContextDesignFactory() : base("Development", "CloudEdgeBilling.BAL")
    {
    }

    protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
    {
        return new ApplicationDbContext(options);
    }
}