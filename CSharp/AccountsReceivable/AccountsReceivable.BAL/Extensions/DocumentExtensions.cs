using AutoMapper;
using AccountsReceivable.BAL.Data;
using Microsoft.EntityFrameworkCore;
using AccountsReceivable.BL.Models.Json;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BAL.Extensions;

public static class DocumentExtensions
{
    // ReSharper disable once UnusedMember.Global - UiPath Hook
    public static readonly MapperConfiguration Configuration = new(cfg =>
    {
        /* Document */

        var documentMap = cfg.CreateMap<DocumentDto, Document>();

        documentMap.ForMember(document => document.Id, opt => opt.MapFrom(src => src.Document));
        documentMap.ForMember(document => document.PlantName, opt => opt.Ignore());
        documentMap.ForMember(document => document.PreviousDocument, opt => opt.Ignore());
        documentMap.ForMember(document => document.Animals, opt => opt.MapFrom(src => src.Animal));
        documentMap.ForMember(document => document.PreviousDocumentId, opt => opt.MapFrom(src => src.PreviousDocument));
        documentMap.ForMember(document => document.AnimalTypeSummaries, opt => opt.MapFrom(src => src.PaymentAdviceAnimalType));

        documentMap.ForMember(document => document.StockCount, opt => opt.MapFrom(src => src.PaymentAdviceTotalStockReceived));
        documentMap.ForMember(document => document.WeightTotal, opt => opt.MapFrom(src => src.PaymentAdviceTotalMeatKg));
        documentMap.ForMember(document => document.WeightCostTotal, opt => opt.MapFrom(src => src.PaymentAdviceTotalPricePaid));
        documentMap.ForMember(document => document.PremiumCostTotal, opt => opt.MapFrom(src => src.AdditionalPremiumsDeductions));
        documentMap.ForMember(document => document.DeductionCostTotal, opt => opt.MapFrom(src => src.PaymentSummaryAdvanceTotalDeductions));
        documentMap.ForMember(document => document.NetCostTotal, opt => opt.MapFrom(src => src.NetAdvance));

        var summaryMap = cfg.CreateMap<PaymentAdviceAnimalTypeDto, AnimalTypeSummary>();
        summaryMap.ForMember(animalTypeSummary => animalTypeSummary.AnimalType, opt => opt.Ignore());
        summaryMap.ForMember(animalTypeSummary => animalTypeSummary.StockCount, opt => opt.MapFrom(src => src.AnimalTypeTotalStockReceived));
        summaryMap.ForMember(animalTypeSummary => animalTypeSummary.StockWeightKg, opt => opt.MapFrom(src => src.AnimalTypeTotalMeatKg));
        summaryMap.ForMember(animalTypeSummary => animalTypeSummary.StockCost, opt => opt.MapFrom(src => src.AnimalTypeTotalPricePaid));

        var animalMap = cfg.CreateMap<AnimalDto, Animal>();
        animalMap.ForMember(animal => animal.Grade, opt => opt.Ignore());
        animalMap.ForMember(animal => animal.DeductionDetails, opt => opt.MapFrom(src => src.AnimalPaymentSummaryDetail));
        animalMap.ForMember(animal => animal.MeetsMasterGrade, opt => opt.MapFrom(src => src.MeetsMasterGrade!.Equals("*")));
        animalMap.ForMember(animal => animal.Defects, opt => opt.MapFrom(src => src.Defects.Select(d => d.DefectDescription)));
        animalMap.ForMember(animal => animal.PremiumDetails, opt => opt.MapFrom(src => src.AnimalAdditionalPremiumsDeductionsDetail));

        animalMap.ForMember(animal => animal.WeightCost, opt => opt.MapFrom(src => src.PaymentAdvicePricePaid));
        animalMap.ForMember(animal => animal.PremiumCost, opt => opt.MapFrom(src => src.AnimalAdditionalPremiumsDeductionsDetail.Sum(d => d.PaymentSummaryAmount)));
        animalMap.ForMember(animal => animal.DeductionCost, opt => opt.MapFrom(src => src.AnimalPaymentSummaryDetail.Sum(d => d.PaymentSummaryAmount)));
        animalMap.ForMember(animal => animal.NetCost, opt => opt.MapFrom(src =>
            src.PaymentAdvicePricePaid +
            src.AnimalAdditionalPremiumsDeductionsDetail.Sum(d => d.PaymentSummaryAmount) +
            src.AnimalPaymentSummaryDetail.Sum(d => d.PaymentSummaryAmount)));

        cfg.CreateMap<AnimalPaymentSummaryDetailDto, DeductionDetail>();
        cfg.CreateMap<AnimalAdditionalPremiumsDeductionsDetailDto, PremiumDetail>();

        cfg.ValueTransformers.Add<string?>(val => string.IsNullOrWhiteSpace(val) ? null : val);
        cfg.ValueTransformers.Add<DateTime?>(val => val.Equals(new DateTime(1900, 1, 1)) ? null : val);
    });

    // ReSharper disable once UnusedMember.Global - UiPath Hook
    public static void CalculatePrices(Document baseDocument, ApplicationDbContext dbContext)
    {
        Task.Run(() => CalculatePricesAsync(baseDocument, dbContext)).GetAwaiter().GetResult();
    }
    
    public static async Task CalculatePricesAsync(this Document baseDocument, ApplicationDbContext dbContext, Schedule? schedule = null)
    {
        var document = await dbContext.Documents
            .Include(entity => entity.Plant)
            .Include(entity => entity.Animals!)
            .ThenInclude(entity => entity.Grade)
            .SingleAsync(entity => entity.Id.Equals(baseDocument.Id)
            );

        schedule ??= await dbContext.Schedules
            .Include(entity => entity.Prices)
            .Include(entity => entity.Uplifts)
            .Where(entity => entity.StatusId == StatusId.Approved || entity.StatusId == StatusId.Overridden)
            .SingleOrDefaultAsync(entity =>
                entity.MeatworkName.Equals(document.Plant.MeatworkName) &&
                entity.StartDate <= document.DateProcessed &&
                entity.EndDate >= document.DateProcessed
            );

        document.CalcWeightCostTotal = 0M;
        document.CalcDeductionCostTotal = 0M;
        document.CalcPremiumCostTotal = 0M;
        document.CalcNetCostTotal = 0M;

        if (schedule is not null)
        {
            foreach (var animal in document.Animals!)
            {
                var schedulePrice = schedule.Prices.Where(price =>
                    price.GradeId == animal.GradeId &&
                    price.MaxWeight >= animal.Weight
                ).MinBy(price => price.MaxWeight);

                if (schedulePrice is null)
                {
                    continue;
                }

                var upliftArray = schedule.Uplifts.Where(uplift =>
                    uplift.AnimalTypeId == animal.Grade.AnimalTypeId &&
                    uplift.MinWeight <= animal.Weight &&
                    uplift.MaxWeight >= animal.Weight
                ).ToArray();

                animal.CalcPrice = Math.Round(schedulePrice.Cost + (schedulePrice.Cost != 0 ? upliftArray.Sum(uplift => uplift.Rate) : 0), 2);
                animal.CalcWeightCost = Math.Round(animal.Weight * animal.CalcPrice, 2);
                animal.CalcDeductionCost = Math.Round(animal.DeductionCost, 2); // TODO
                animal.CalcPremiumCost = Math.Round(animal.PremiumCost, 2); // TODO
                animal.CalcNetCost = Math.Round(animal.CalcWeightCost + animal.CalcDeductionCost + animal.CalcPremiumCost, 2);

                document.CalcWeightCostTotal += animal.CalcWeightCost;
                document.CalcDeductionCostTotal += animal.CalcDeductionCost;
                document.CalcPremiumCostTotal += animal.CalcPremiumCost;
                document.CalcNetCostTotal += animal.CalcNetCost;
            }
            
            document.ScheduleId = schedule.Id;
            document.CalcTimestamp = DateTime.Now;
            document.CalcValidationId = decimal.Compare(document.CalcNetCostTotal, document.NetCostTotal) switch
            {
                -1 => ValidationId.Low,
                0 => ValidationId.Valid,
                1 => ValidationId.High,
                _ => throw new NotImplementedException()
            };
        }
        else
        {
            document.ScheduleId = null;
            document.CalcTimestamp = null;
            document.CalcValidationId = ValidationId.Pending;
        }
    }
}