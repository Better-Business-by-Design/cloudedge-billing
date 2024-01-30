using System.Data;
using System.Globalization;
using CloudEdgeBilling.API.Shared.FluidValidation;
using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using Newtonsoft.Json;
using UiPathApi.Swagger.Api;
using UiPathApi.Swagger.Model;

namespace CloudEdgeBilling.API.Pages;

public partial class Robot : ComponentBase
{
    private const long ProcessId = 23346;
    private readonly CloudEdgeBillingJobArguments _jobArguments = new();

    private readonly List<JobDto> _jobs = new();

    private MudForm _form = null!;

    private ReleaseDto? _releaseDto;

    private CloudEdgeBillingJobArgumentsFluidValidator _validator = null!;
    [Inject] private ApplicationDbContext DbContext { get; set; } = null!;

    [Inject] private ReleasesApi ReleasesApi { get; set; } = null!;
    [Inject] private JobsApi JobsApi { get; set; } = null!;


    public List<ChartSeries> Series = new();

    public string[] XAxisLabels = { "Aug", "Sept", "Oct", "Nov" };

    private bool HasChartDataLoaded = false;

    protected override async Task OnInitializedAsync()
    {
        _validator = new CloudEdgeBillingJobArgumentsFluidValidator();
        _releaseDto = await ReleasesApi.ReleasesGetByIdAsync(ProcessId);
        
        Series.Add(new ChartSeries() {Name = "Invoices", Data = new double[4]});
        
        for (var i = 3; i >= 0; i--)
        {
            var specificMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-(i+1));
            Series[0].Data[3 - i] = Convert.ToDouble(DbContext.Invoices
                .Where(inv => inv.DateTime.Equals(specificMonth))
                .Sum(inv => inv.TotalCost));
        }

        HasChartDataLoaded = true;
        
        await base.OnInitializedAsync();
    }

    private async Task OnClick()
    {
        var process = _releaseDto ?? throw new ArgumentNullException(nameof(_releaseDto));

        await _form.Validate();

        if (_form.IsValid)
        {
            var startJobsRequest = new StartJobsRequest(new StartProcessDto
                {
                    ReleaseKey = process.Key,
                    Strategy = StartProcessDto.StrategyEnum.ModernJobsCount,
                    JobsCount = 1,
                    InputArguments = JsonConvert.SerializeObject(_jobArguments)
                }
            );
            var jobODataValue = await JobsApi.JobsStartJobsAsync(startJobsRequest);
            var job = jobODataValue.Value.First();
            _jobs.Add(job);

            StateHasChanged();
        }
    }

    private void RemoveJob(JobDto job)
    {
        _jobs.Remove(job);
        StateHasChanged();
    }

    public record CloudEdgeBillingJobArguments : IDataRow
    {
        // ReSharper disable once InconsistentNaming
        public DateTime? in_StartRangeDateTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
}