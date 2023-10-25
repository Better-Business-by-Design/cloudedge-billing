using System.ComponentModel;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Json;
using AutoMapper;
using Newtonsoft.Json;

namespace AccountsReceivable.BAL.Mappings;

public class DocumentMap
{
    public static MapperConfiguration Configuration { get; set; }
    private static decimal _gst = 0.15m;

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

            documentMap.ForMember(document => document.StockTotal, opt => opt.MapFrom(src => src.PaymentAdviceTotalStockReceived));
            documentMap.ForMember(document => document.StockWeightTotal, opt => opt.MapFrom(src => src.PaymentAdviceTotalMeatKg));
            documentMap.ForMember(document => document.WeightCostTotal, opt => opt.MapFrom(src => src.PaymentAdviceTotalPricePaid));
            documentMap.ForMember(document => document.PremiumCostTotal, opt => opt.MapFrom(src => src.AdditionalPremiumsDeductions));
            documentMap.ForMember(document => document.DeductionCostTotal, opt => opt.MapFrom(src => src.PaymentSummaryAdvanceTotalDeductions));
            documentMap.ForMember(document => document.NetCostTotal, opt => opt.MapFrom(src => src.NetAdvance));
            documentMap.ForMember(document => document.GstCostTotal, opt => opt.MapFrom(src => src.GstOnOutputs + src.GstOnInputs));
            documentMap.ForMember(document => document.GrossCostTotal, opt => opt.MapFrom(src => src.Total));

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

            animalMap.ForMember(animal => animal.StockWeight, opt => opt.MapFrom(src => src.Weight));
            animalMap.ForMember(animal => animal.WeightCost, opt => opt.MapFrom(src => src.PaymentAdvicePricePaid));
            animalMap.ForMember(animal => animal.PremiumCost, opt => opt.MapFrom(src => src.AnimalAdditionalPremiumsDeductionsDetail.Sum(d => d.PaymentSummaryAmount)));
            animalMap.ForMember(animal => animal.DeductionCost, opt => opt.MapFrom(src => src.AnimalPaymentSummaryDetail.Sum(d => d.PaymentSummaryAmount)));
            animalMap.ForMember(animal => animal.NetCost, opt => opt.MapFrom(src =>
                src.PaymentAdvicePricePaid +
                src.AnimalAdditionalPremiumsDeductionsDetail.Sum(d => d.PaymentSummaryAmount) +
                src.AnimalPaymentSummaryDetail.Sum(d => d.PaymentSummaryAmount)));
            animalMap.ForMember(animal => animal.GstCost, opt => opt.MapFrom(src => Math.Round((
                src.PaymentAdvicePricePaid +
                src.AnimalAdditionalPremiumsDeductionsDetail.Sum(d => d.PaymentSummaryAmount) +
                src.AnimalPaymentSummaryDetail.Sum(d => d.PaymentSummaryAmount)
            ) * _gst, 2)));
            animalMap.ForMember(animal => animal.GrossCost, opt => opt.MapFrom(src => Math.Round((
                src.PaymentAdvicePricePaid +
                src.AnimalAdditionalPremiumsDeductionsDetail.Sum(d => d.PaymentSummaryAmount) +
                src.AnimalPaymentSummaryDetail.Sum(d => d.PaymentSummaryAmount)
            ) * (1 + _gst), 2)));

            cfg.CreateMap<AnimalPaymentSummaryDetailDto, DeductionDetail>();
            cfg.CreateMap<AnimalAdditionalPremiumsDeductionsDetailDto, PremiumDetail>();

            cfg.ValueTransformers.Add<string?>(val => string.IsNullOrWhiteSpace(val) ? null : val);
            cfg.ValueTransformers.Add<DateTime?>(val => val.Equals(new DateTime(1900, 1, 1)) ? null : val);
        });
    }
}