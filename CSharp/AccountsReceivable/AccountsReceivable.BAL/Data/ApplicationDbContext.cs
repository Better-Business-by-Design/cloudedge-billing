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
        // Calculations

        /*var documents = Documents!
            .Include(document => document.BusinessLogic)
            .Include(document => document.Animal)
            .ThenInclude(animal => animal.BusinessLogic)
            .Where(document => document.BusinessLogic.ScheduleId != null)
            .ToList();

        foreach (var document in documents)
        {
            var schedule = Schedules
                .Include(schedule => schedule.BusinessLogic)
                .Include(schedule => schedule.Pricing)
                .ThenInclude(pricing => pricing.BusinessLogic)
                .First(schedule => schedule.Id == document.BusinessLogic.ScheduleId);

            document.BusinessLogic.Gst = 0;
            document.BusinessLogic.Total = 0;

            foreach (var animal in document.Animal)
            {
                var bl = animal.BusinessLogic;

                var net = animal.Weight * schedule.Pricing.First(p =>
                    p.BusinessLogic.GradeId == bl.GradeId && p.MinWeight <= animal.Weight &&
                    animal.Weight <= p.MaxWeight).Price;
                bl.Gst = net * (3/23);
                bl.Total = net;
                bl.ValidationId = bl.Total < animal.Price ? ValidationId.Low :
                    bl.Total > animal.Price ? ValidationId.High : ValidationId.Valid;

                document.BusinessLogic.Gst += bl.Gst;
                document.BusinessLogic.Total += bl.Total;
            }

            document.BusinessLogic.ValidationId = document.BusinessLogic.Total < document.PaymentAdviceTotalPricePaid ? ValidationId.Low :
                document.BusinessLogic.Total > document.PaymentAdviceTotalPricePaid ? ValidationId.High : ValidationId.Valid;
        }

        SaveChanges();*/

        /*
        foreach (var document in documents)
        {
            document.BusinessLogic.StockTotal =
                Set<AnimalDto>().Count(animal => animal.DocumentDocument == document.Document);
            document.BusinessLogic.StockWeight = Set<AnimalDto>()
                .Where(animal => animal.DocumentDocument == document.Document).Sum(animal => animal.Weight);
        }

        SaveChanges();*/

        /*// Pricing Distribution

        var schedules = Schedules!
            .AsNoTracking()
            .Include(schedule => schedule.BusinessLogic)
            .Where(schedule => schedule.BusinessLogic.StatusId == StatusId.Approved)
            .ToArray();

        // BCI Calculation

        foreach (var schedule in schedules)
        {
            var documents = Documents!
                .Include(document => document.BusinessLogic)
                .Include(document => document.BusinessLogic.Plant)
                .Where(document =>
                    document.BusinessLogic.StatusId == StatusId.Pending &&
                    document.BusinessLogic.Plant.MeatworkName == schedule.BusinessLogic.MeatworkName &&
                    schedule.StartDate <= document.DateProcessed &&
                    document.DateProcessed <= schedule.EndDate
                )
                .ToArray();

            foreach (var document in documents)
            {
                document.BusinessLogic.ScheduleId = schedule.Id;
            }
        }

        SaveChanges();*/
    }

    public DbSet<Document> Documents { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS");

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
                    uplift.Name
                })
                .HasDatabaseName("Uplift_Unique")
                .IsUnique();

            entity.ToTable(nameof(Uplift), "application");
        });

        modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(document => new { document.Id });

                entity.Property(document => document.SupplierComments).HasConversion(
                    convertCollection => string.Join(",", convertCollection),
                    convertString => convertString.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    GetStringValueComparer()
                );

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
                    collection => string.Join(",", collection),
                    convertString => convertString.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    GetStringValueComparer()
                );

                entity.ToTable(nameof(Animal), "application");
            }
        );

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
            }
        );

        #endregion

        #region Source

        modelBuilder.Entity<TransitDto>(entity =>
            {
                entity.HasKey(transit => transit.Id);
                entity.Property(transit => transit.Id).ValueGeneratedOnAdd();

                entity.ToTable(nameof(TransitDto), "source");
            }
        );

        modelBuilder.Entity<DeductionDto>(entity =>
            {
                entity.HasKey(deduction => deduction.Id);
                entity.Property(deduction => deduction.Id).ValueGeneratedOnAdd();

                entity.ToTable(nameof(DeductionDto), "source");
            }
        );

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