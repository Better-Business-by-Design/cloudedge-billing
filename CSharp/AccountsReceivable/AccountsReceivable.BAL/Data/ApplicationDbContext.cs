using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using AccountsReceivable.BL.Models.Source;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccountsReceivable.BAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS");
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(entity => entity.GetProperties()).Where(property => property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(9);
            property.SetScale(2);
        }

        #region Account

        modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.EmailAddress);

                entity.ToTable(nameof(User), "account");
            }
        );

        modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasKey(audit => new { audit.UserId, audit.Timestamp });

                entity.ToTable(nameof(Audit), "account");
            }
        );

        #endregion

        #region Application

        modelBuilder.Entity<Meatwork>(entity =>
            {
                entity.HasKey(meatwork => meatwork.Name);

                entity.ToTable(nameof(Meatwork), "application");
            }
        );

        modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(plant => plant.Name);

                entity.ToTable(nameof(Plant), "application");
            }
        );

        modelBuilder.Entity<Farm>(entity =>
            {
                entity.HasKey(farm => farm.CostCentre);

                entity.ToTable(nameof(Farm), "application");
            }
        );

        modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(supplier => new { supplier.FarmCostCentre, supplier.MeatworkName });

                entity.ToTable(nameof(Supplier), "application");
            }
        );

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(schedule => schedule.Id);
            entity.Property(schedule => schedule.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(10000L);

            entity.HasIndex(supplier => new
                {
                    supplier.StartDate,
                    supplier.EndDate,
                    supplier.MeatworkName
                })
                .HasDatabaseName("Schedule_Unique")
                .IsUnique();

            entity.ToTable(nameof(Schedule), "application");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(price => price.Id);
            entity.Property(price => price.Id).ValueGeneratedOnAdd();

            entity.HasIndex(price => new
                {
                    price.ScheduleId,
                    price.GradeId,
                    price.MinWeight,
                    price.MaxWeight
                })
                .HasDatabaseName("Price_Unique")
                .IsUnique();

            entity.ToTable(nameof(Price), "application");
        });

        modelBuilder.Entity<Uplift>(entity =>
        {
            entity.HasKey(uplift => uplift.Id);
            entity.Property(uplift => uplift.Id).ValueGeneratedOnAdd();

            entity.HasIndex(uplift => new
                {
                    uplift.ScheduleId,
                    uplift.Name,
                    uplift.AnimalTypeId
                })
                .HasDatabaseName("Uplift_Unique")
                .IsUnique();

            entity.ToTable(nameof(Uplift), "application");
        });

        modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(document => new { document.Id });

                entity.ToTable(nameof(Document), "application");
            }
        );

        modelBuilder.Entity<AnimalTypeSummary>(entity =>
            {
                entity.HasKey(advice => new
                {
                    advice.DocumentId,
                    advice.AnimalTypeId
                });

                entity.ToTable(nameof(AnimalTypeSummary), "application");
            }
        );

        modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(animal => animal.Id);
                entity.Property(animal => animal.Id).ValueGeneratedOnAdd();

                entity.Property(document => document.Defects).HasConversion(
                    collection => string.Join(",", collection ?? Array.Empty<string>()),
                    convertString => convertString.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    GetStringValueComparer()
                );

                entity.ToTable(nameof(Animal), "application");
            }
        );

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(comment => comment.Id);
            entity.Property(comment => comment.Id).ValueGeneratedOnAdd();
            
            entity.ToTable(nameof(Comment), "application");
        });

        modelBuilder.Entity<Transit>(entity =>
        {
            entity.HasKey(transit => transit.Id);
            entity.Property(transit => transit.Id).ValueGeneratedOnAdd();
            
            entity.ToTable(nameof(Transit), "application");
        });
        
        modelBuilder.Entity<DeductionDetail>(entity =>
            {
                entity.HasKey(deduction => new { deduction.AnimalId, deduction.Code });

                entity.ToTable(nameof(DeductionDetail), "application");
            }
        );

        modelBuilder.Entity<PremiumDetail>(entity =>
            {
                entity.HasKey(premium => new { premium.AnimalId, premium.Code });

                entity.ToTable(nameof(PremiumDetail), "application");
            }
        );

        #endregion

        #region Enum

        modelBuilder.Entity<SpeciesType>(entity =>
            {
                entity.HasKey(speciesType => speciesType.Id);

                entity.HasData(
                    Enum.GetValues(typeof(SpeciesTypeId))
                        .Cast<SpeciesTypeId>()
                        .Select(SpeciesTypeHelper.GetInfo)
                );

                entity.ToTable(nameof(SpeciesType), "enum");

                /* Prevent Cascade */

                entity.HasMany<AnimalType>()
                    .WithOne(animalType => animalType.SpeciesType)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany<Document>()
                    .WithOne(document => document.SpeciesType)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        );

        modelBuilder.Entity<AnimalType>(entity =>
            {
                entity.HasKey(animalType => animalType.Id);

                entity.HasData(
                    Enum.GetValues(typeof(AnimalTypeId))
                        .Cast<AnimalTypeId>()
                        .Select(AnimalTypeHelper.GetInfo)
                );

                entity.ToTable(nameof(AnimalType), "enum");

                /* Prevent Cascade */

                entity.HasMany<Grade>()
                    .WithOne(grade => grade.AnimalType)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany<AnimalTypeSummary>()
                    .WithOne(animalTypeSummary => animalTypeSummary.AnimalType)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany<Uplift>()
                    .WithOne(uplift => uplift.AnimalType)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        );

        modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(grade => grade.Id);

                entity.HasData(
                    Enum.GetValues(typeof(GradeId))
                        .Cast<GradeId>()
                        .Select(GradeHelper.GetInfo)
                );

                entity.ToTable(nameof(Grade), "enum");

                /* Prevent Cascade */

                entity.HasMany<Animal>()
                    .WithOne(animal => animal.Grade)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany<Price>()
                    .WithOne(price => price.Grade)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        );

        modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(status => status.Id);

                entity.HasData(
                    Enum.GetValues(typeof(StatusId))
                        .Cast<StatusId>()
                        .Select(StatusHelper.GetInfo)
                );

                entity.ToTable(nameof(Status), "enum");
                
                /* Prevent Cascade */

                entity.HasMany<Document>()
                    .WithOne(document => document.Status)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasMany<Schedule>()
                    .WithOne(schedule => schedule.Status)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        );

        modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(role => role.Id);

                entity.HasData(
                    Enum.GetValues(typeof(RoleId))
                        .Cast<RoleId>()
                        .Select(RoleHelper.GetInfo)
                );

                entity.ToTable(nameof(Role), "enum");
                
                /* Prevent Cascade */

                entity.HasMany<User>()
                    .WithOne(user => user.Role)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        );

        modelBuilder.Entity<Validation>(entity =>
            {
                entity.HasKey(validation => validation.Id);

                entity.HasData(
                    Enum.GetValues(typeof(ValidationId))
                        .Cast<ValidationId>()
                        .Select(ValidationHelper.GetInfo)
                );

                entity.ToTable(nameof(Validation), "enum");
                
                /* Prevent Cascade */

                entity.HasMany<Document>()
                    .WithOne(document => document.CalcValidation)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasMany<Animal>()
                    .WithOne(animal => animal.Validation)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        );

        #endregion

        /*#region Source

        modelBuilder.Entity<DeductionDto>(entity =>
            {
                entity.HasKey(deduction => deduction.Id);
                entity.Property(deduction => deduction.Id).ValueGeneratedOnAdd();

                entity.ToTable(nameof(DeductionDto), "source");
            }
        );

        #endregion*/

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