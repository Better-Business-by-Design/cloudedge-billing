﻿using AccountsReceivable.BL.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccountsReceivable.BAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<LineItem> LineItems { get; set; } = null!;
    
    public DbSet<PayMonthlyPlan> PayMonthlyPlans { get; set; } = null!;

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
                
                entity.HasOne<PayMonthlyPlan>(customer => customer.PayMonthlyPlan)
                    .WithMany(plan => plan.Customers)
                    .HasForeignKey(customer => customer.PayMonthlyPlanId).HasConstraintName("FK_Customer")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);
                
                entity.ToTable("customers", "cloudedge");
            });

        modelBuilder.Entity<PayMonthlyPlan>(entity =>
        {
            entity.HasKey(plan => plan.PlanId).HasName("pay_monthly_plans_PK");
            
            entity.HasMany(plan => plan.Customers)
                .WithOne(customer => customer.PayMonthlyPlan)
                .HasForeignKey(customer => customer.PayMonthlyPlanId).HasConstraintName("FK_Customer")
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
            
            entity.ToTable("pay_monthly_plans", "cloudedge");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.HasKey(item => item.Id).HasName("PK_line_items");

            entity.HasOne<Customer>(item => item.Customer)
                .WithMany(customer => customer.LineItems)
                .HasForeignKey(item => item.CustomerId).HasConstraintName("line_items_FK")
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.ToTable("line_items", "cloudedge");
        });

        #endregion

        // #region Enum
        //
        // #endregion

        base.OnModelCreating(modelBuilder);
    }

    public async Task AddValue(IDataRow value)
    {
        switch (value)
        {
            case Customer c:
                await Customers.AddAsync(c);
                break;
            case LineItem l:
                await LineItems.AddAsync(l);
                break;
            case PayMonthlyPlan p:
                await PayMonthlyPlans.AddAsync(p);
                break;
            case null:
                throw new ArgumentNullException(nameof(value), "Tried to add null value to dataset");
            default:
                throw new NotImplementedException($"{value.GetType()} Not implemented in Add Values");
        }
        await SaveChangesAsync();
    }

    public async Task AddValues(IEnumerable<IDataRow> values)
    {
        var addValues = new List<IDataRow>(values);
        if (!addValues.Any()) return;
        
        switch (addValues.First())
        {
            case Customer _:
                await Customers.AddRangeAsync(addValues.Cast<Customer>());
                break;
            case LineItem _:
                await LineItems.AddRangeAsync(addValues.Cast<LineItem>());
                break;
            case PayMonthlyPlan _:
                await PayMonthlyPlans.AddRangeAsync(addValues.Cast<PayMonthlyPlan>());
                break;
            case null:
                throw new ArgumentNullException(nameof(addValues), "Tried to add null values to dataset");
            default:
                throw new NotImplementedException($"{addValues.First().GetType()} Not implemented in Add Values");
        }
        await SaveChangesAsync();
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
                        .SetProperty(c => c.ParentName, customer.ParentName)
                        .SetProperty(c => c.CustomerName, customer.CustomerName)
                        .SetProperty(c => c.DomainUuid, customer.DomainUuid)
                        .SetProperty(c => c.InvoiceName, customer.InvoiceName)
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
                );
                break;
            case PayMonthlyPlan payMonthlyPlan :
                Entry(value).CurrentValues.SetValues(payMonthlyPlan);
                results = await PayMonthlyPlans.Where(p => p.PlanId == ((PayMonthlyPlan)value).PlanId)
                    .ExecuteUpdateAsync(
                        setters => setters
                            .SetProperty(p => p.PlanName, payMonthlyPlan.PlanName)
                            .SetProperty(p => p.LocalSize, payMonthlyPlan.LocalSize)
                            .SetProperty(p => p.NationalSize, payMonthlyPlan.NationalSize)
                            .SetProperty(p => p.MobileSize, payMonthlyPlan.MobileSize)
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


    // private static ValueComparer<ICollection<string>> GetStringValueComparer()
    // {
    //     return new ValueComparer<ICollection<string>>(
    //         (collection1, collection2) => collection1!.SequenceEqual(collection2!),
    //         collection => collection.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
    //         collection => collection.ToList());
    // }
}