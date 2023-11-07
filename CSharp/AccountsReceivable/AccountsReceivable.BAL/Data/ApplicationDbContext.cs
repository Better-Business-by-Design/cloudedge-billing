﻿using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
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

        #region Account

        #endregion

        #region Application

        modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(customer => customer.Id);
                entity.Property(customer => customer.Id).ValueGeneratedOnAdd();
                
                entity.Property(customer => customer.HasCustomRates)
                    .HasConversion(
                        v => v == true ? "Y" : "N",
                        v => v.ToUpper().Equals("Y"));
                entity.Property(customer => customer.HasCustomPackage)
                    .HasConversion(
                        v => v == true ? "Y" : "N", 
                        v => v.ToUpper().Equals("Y"));
                
                
                entity.HasOne<PayMonthlyPlan>(customer => customer.PayMonthlyPlan)
                    .WithMany(plan => plan.Customers)
                    .HasForeignKey(customer => customer.PayMonthlyPlanId)
                    .OnDelete(DeleteBehavior.SetNull);
                
                entity.ToTable("customers", "cloudedge");
            });

        modelBuilder.Entity<PayMonthlyPlan>(entity =>
        {
            entity.HasKey(plan => plan.PlanId);
            entity.HasMany<Customer>(plan => plan.Customers)
                .WithOne(customer => customer.PayMonthlyPlan)
                .OnDelete(DeleteBehavior.SetNull);
            entity.ToTable("pay_monthly_plans", "cloudedge");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedOnAdd();
            
            entity.HasOne<Customer>(item => item.Customer)
                .WithMany(customer => customer.LineItems)
                .HasForeignKey(item => item.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
            
            entity.ToTable("line_items", "cloudedge");
        });

        #endregion

        #region Enum

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    private static ValueComparer<ICollection<string>> GetStringValueComparer()
    {
        return new ValueComparer<ICollection<string>>(
            (collection1, collection2) => collection1!.SequenceEqual(collection2!),
            collection => collection.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            collection => collection.ToList());
    }
}