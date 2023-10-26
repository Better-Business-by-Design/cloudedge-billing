using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Json;
using Microsoft.EntityFrameworkCore;
using AccountsReceivable.BAL.Data;
using AutoMapper;

namespace AccountsReceivable.BAL.Mappings;

public static class DocumentMap
{
    public static MapperConfiguration Configuration { get; set; }

    static DocumentMap()
    {
        Configuration = new MapperConfiguration(cfg =>
        {
            /* Document */

            var documentMap = cfg.CreateMap<DocumentDto, Document>();

            documentMap.ForMember(document => document.Id, opt => opt.MapFrom(src => src.Document));
            documentMap.ForMember(document => document.PlantName, opt => opt.Ignore());
            documentMap.ForMember(document => document.PreviousDocument, opt => opt.Ignore());
            documentMap.ForMember(document => document.Animals, opt => opt.MapFrom(src => src.Animal));
            documentMap.ForMember(document => document.PreviousDocumentId, opt => opt.MapFrom(src => src.PreviousDocument));
            documentMap.ForMember(document => document.AnimalTypeSummaries, opt => opt.MapFrom(src => src.PaymentAdviceAnimalType));

            documentMap.ForMember(document => document.AnimalTotal, opt => opt.MapFrom(src => src.PaymentAdviceTotalStockReceived));
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
    }

    public static async Task CalculateDocument(ApplicationDbContext context, string documentId)
    {
        var document = await context.Documents
            .Include(document => document.Plant)
            .Include(document => document.Animals!)
            .ThenInclude(animal => animal.Grade)
            .SingleAsync(document => document.Id.Equals(documentId));

        var schedule = await context.Schedules
            .Include(schedule => schedule.Prices)
            .Include(schedule => schedule.Uplifts)
            .SingleOrDefaultAsync(schedule =>
                schedule.MeatworkName.Equals(document.Plant.MeatworkName) &&
                schedule.StartDate <= document.DateProcessed &&
                schedule.EndDate >= document.DateProcessed
            );

        if (schedule is null)
            return;

        var stockWeightCost = 0M;
        var deduction = 0M;
        var premium = 0M;
        var net = 0M;

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

            animal.CalcPrice = schedulePrice.Cost + upliftArray.Sum(uplift => uplift.Rate);
            animal.CalcWeightCost = animal.Weight * animal.CalcPrice;
            animal.CalcDeductionCost = animal.DeductionCost; // TODO
            animal.CalcPremiumCost = animal.PremiumCost; // TODO
            animal.CalcNetCost = animal.CalcWeightCost + animal.CalcDeductionCost + animal.CalcPremiumCost;

            stockWeightCost += animal.CalcWeightCost;
            deduction += animal.CalcDeductionCost;
            premium += animal.CalcPremiumCost;
            net += animal.CalcNetCost;
        }

        document.AnimalTotal = (ushort) document.Animals.Count;
        document.CalcWeightCostTotal = stockWeightCost;
        document.CalcDeductionCostTotal = deduction;
        document.CalcPremiumCostTotal = premium;
        document.CalcNetCostTotal = net;

        document.ScheduleId = schedule.Id;
        document.CalcTimestamp = DateTime.Now;

        await context.SaveChangesAsync();
    }

    public static async Task CalculateDocuments(ApplicationDbContext context, IEnumerable<string> documentIds)
    {
        documentIds = context.Documents.Select(d => d.Id).ToArray();

        foreach (var documentId in documentIds) await CalculateDocument(context, documentId);
    }
}