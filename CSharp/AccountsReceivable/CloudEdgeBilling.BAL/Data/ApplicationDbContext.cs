using CloudEdgeBilling.BL.Models.Application;
using CloudEdgeBilling.BL.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudEdgeBilling.BAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<LineItem> LineItems { get; set; } = null!;
    
    public DbSet<PayMonthlyPlan> PayMonthlyPlans { get; set; } = null!;

    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Branding> BrandingThemes { get; set; } = null!;

    public DbSet<Invoice> Invoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS");
        
        var decimalEntities = modelBuilder.Model.GetEntityTypes()
            .SelectMany(entity => entity.GetProperties())
            .Where(property => property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?));
        
        foreach (var property in decimalEntities)
        {
            property.SetPrecision(9);
            property.SetScale(3);
        }

        // #region Account
        //
        // #endregion

        #region Application

        modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(customer => customer.Id).HasName("PK_Customer");
                
                entity.ToTable("customers", "dbo");
            });

        modelBuilder.Entity<PayMonthlyPlan>(entity =>
        {
            entity.HasKey(plan => plan.PlanId).HasName("pay_monthly_plans_PK");
            
            entity.ToTable("pay_monthly_plans", "dbo");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.HasKey(item => item.Id).HasName("line_items_PK");
            
            entity.ToTable("line_items", "dbo");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(account => account.AccountId).HasName("Account_PK");
            entity.HasAlternateKey(account => account.Code).HasName("Account_UN");

            entity.ToTable("Account", "dbo");
        });

        modelBuilder.Entity<Branding>(entity =>
        {
            entity.HasKey(branding => branding.Id).HasName("Branding_PK");
            entity.ToTable("Branding", "dbo");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(invoice => new { invoice.CustomerId, invoice.DateTime });
            entity.ToTable("invoices", "dbo");
        });

        #endregion

        #region Enum

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(business => business.BusinessId).HasName("Business_PK");
            entity.HasData(
                Enum.GetValues(typeof(BusinessId))
                    .Cast<BusinessId>()
                    .Select(BusinessHelper.GetInfo)
            );

            entity.ToTable(nameof(Business), "dbo");
        });
        
        #endregion

        base.OnModelCreating(modelBuilder);
    }

    public async Task AddValue(IDataRow value, bool isNew = true)
    {
        switch (value)
        {
            case Customer c:
                await Customers.AddAsync(c);
                if (isNew)
                {
                    await SaveChangesAsync();
                }
                else
                {
                    await SaveChangesWithIdentityInsertAsync<Customer>();
                }
                break;
            case LineItem l:
                await LineItems.AddAsync(l);
                if (isNew)
                {
                    await SaveChangesAsync();
                }
                else
                {
                    await SaveChangesWithIdentityInsertAsync<LineItem>();
                }
                break;
            case PayMonthlyPlan p:
                await PayMonthlyPlans.AddAsync(p);
                if (isNew)
                {
                    await SaveChangesAsync();
                }
                else
                {
                    await SaveChangesWithIdentityInsertAsync<PayMonthlyPlan>();
                }
                break;
            case null:
                throw new ArgumentNullException(nameof(value), "Tried to add null value to dataset");
            default:
                throw new NotImplementedException($"{value.GetType()} Not implemented in Add Values");
        }
    }

    public async Task AddValues(IEnumerable<IDataRow> values, bool isNew = true)
    {
        var addValues = new List<IDataRow>(values);
        if (!addValues.Any()) return;
        
        switch (addValues.First())
        {
            case Customer _:
                await Customers.AddRangeAsync(addValues.Cast<Customer>());
                if (isNew)
                {
                    await SaveChangesAsync();
                }
                else
                {
                    await SaveChangesWithIdentityInsertAsync<Customer>();
                }
                break;
            case LineItem _:
                await LineItems.AddRangeAsync(addValues.Cast<LineItem>());
                if (isNew)
                {
                    await SaveChangesAsync();
                }
                else
                {
                    await SaveChangesWithIdentityInsertAsync<LineItem>();
                }
                break;
            case PayMonthlyPlan _:
                await PayMonthlyPlans.AddRangeAsync(addValues.Cast<PayMonthlyPlan>());
                if (isNew)
                {
                    await SaveChangesAsync();
                }
                else
                {
                    await SaveChangesWithIdentityInsertAsync<PayMonthlyPlan>();
                }
                break;
            case null:
                throw new ArgumentNullException(nameof(addValues), "Tried to add null values to dataset");
            default:
                throw new NotImplementedException($"{addValues.First().GetType()} Not implemented in Add Values");
        }
    }

    public async Task RemoveValue(IDataRow value)
    {
        switch (value)
        {
            case Customer c:
                Customers.Remove(c);
                break;
            case LineItem l:
                LineItems.Remove(l);
                break;
            case PayMonthlyPlan p:
                PayMonthlyPlans.Remove(p);
                break;
            case null:
                throw new ArgumentNullException(nameof(value), "Tried to remove null value from dataset");
            default:
                throw new NotImplementedException($"{value.GetType()} Not implemented in Remove Value");
        }
        await SaveChangesAsync();
    }
    
    public async Task RemoveValues(IEnumerable<IDataRow> values)
    {

        var removeValues = new List<IDataRow>(values);
        if (!removeValues.Any()) return;
        
        switch (removeValues.First())
            {
                case Customer _:
                    Customers.RemoveRange(removeValues.Cast<Customer>());
                    break;
                case LineItem _:
                    LineItems.RemoveRange(removeValues.Cast<LineItem>());
                    break;
                case PayMonthlyPlan _:
                    PayMonthlyPlans.RemoveRange(removeValues.Cast<PayMonthlyPlan>());
                    break;
                case null:
                    throw new ArgumentNullException(nameof(removeValues), "Tried to remove null value from dataset");
                default:
                    throw new NotImplementedException($"{removeValues.First().GetType()} Not implemented in Remove Values");
            }
        await SaveChangesAsync();
    }

    public async Task EditValue(IDataRow value,IDataRow newValue)
    {
        var results = 0;
        switch (newValue)
        {
            case Customer customer:
                Entry(value).CurrentValues.SetValues(customer);
                results = await Customers.Where(c => c.Id == ((Customer)value).Id).ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(c => c.CustomerName, customer.CustomerName)
                        .SetProperty(c => c.DomainName, customer.DomainName)
                        .SetProperty(c => c.DomainUuid, customer.DomainUuid)
                        .SetProperty(c => c.XeroContactName, customer.XeroContactName)
                        .SetProperty(c => c.PayMonthlyPlanId, customer.PayMonthlyPlanId)
                        .SetProperty(c => c.IsActive, customer.IsActive)
                        .SetProperty(c => c.Location, customer.Location)
                    );
                break;
            case LineItem lineItem:
                Entry(value).CurrentValues.SetValues(lineItem);
                results = await LineItems.Where(l => l.Id == ((LineItem)value).Id).ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(l => l.CustomerId, lineItem.CustomerId)
                        .SetProperty(l => l.Description, lineItem.Description)
                        .SetProperty(l => l.Quantity, lineItem.Quantity)
                        .SetProperty(l => l.UnitPrice, lineItem.UnitPrice)
                        .SetProperty(l => l.Discount, lineItem.Discount)
                        .SetProperty(l => l.AccountId, lineItem.AccountId)
                        .SetProperty(l => l.BusinessId, lineItem.BusinessId)
                );
                break;
            case PayMonthlyPlan payMonthlyPlan :
                Entry(value).CurrentValues.SetValues(payMonthlyPlan);
                results = await PayMonthlyPlans.Where(p => p.PlanId == ((PayMonthlyPlan)value).PlanId)
                    .ExecuteUpdateAsync(
                        setters => setters
                            .SetProperty(p => p.PlanName, payMonthlyPlan.PlanName)
                            .SetProperty(p => p.NationalSize, payMonthlyPlan.NationalSize)
                            .SetProperty(p => p.MobileSize, payMonthlyPlan.MobileSize)
                            .SetProperty(p => p.InternationalSize, payMonthlyPlan.InternationalSize)
                            .SetProperty(p => p.TollFreeLandlineSize, payMonthlyPlan.InternationalSize)
                            .SetProperty(p => p.TollFreeMobileSize, payMonthlyPlan.TollFreeMobileSize)
                            .SetProperty(p => p.Price, payMonthlyPlan.Price)
                            .SetProperty(p => p.MinPrice, payMonthlyPlan.MinPrice)
                        );
                break;
            case null:
                throw new ArgumentNullException(nameof(value), "Tried to update null value in dataset");
            default:
                throw new NotImplementedException($"{newValue.GetType()} not implemented in EditValue");
        }
        
        Console.WriteLine($"{results} changes saved to database.");
    }
    
    public async Task<int> SaveChangesWithIdentityInsertAsync<TEnt>(CancellationToken token = default)
    {
        await using var transaction = await Database.BeginTransactionAsync(token);
        await SetIdentityInsertAsync<TEnt>(true, token);
        var ret = await SaveChangesAsync(token);
        await SetIdentityInsertAsync<TEnt>(false, token);
        await transaction.CommitAsync(token);

        return ret;
    }

    private async Task SetIdentityInsertAsync<TEnt>(bool enable, CancellationToken token)
    {
        var entityType = Model.FindEntityType(typeof(TEnt)) ?? throw new ArgumentNullException(nameof(TEnt));
        var value = enable ? "ON" : "OFF";
        var query = $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}";
        await Database.ExecuteSqlRawAsync(query, token);
    }
    
}
