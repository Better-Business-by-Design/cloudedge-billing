using AccountsReceivable.API.Shared.FluidValidation;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using UiPathApi.Swagger.Api;
using UiPathApi.Swagger.Model;

namespace AccountsReceivable.API.Pages;

public partial class Robot : ComponentBase
{
    [Inject] private ApplicationDbContext DbContext { get; set; } = null!;
    
    [Inject] private ReleasesApi ReleasesApi { get; set; } = null!;
    [Inject] private JobsApi JobsApi { get; set; } = null!;

    private MudForm _form = null!;
    private readonly CloudEdgeBillingJobArguments _jobArguments = new();
    
    private ReleaseDto? _releaseDto;

    private readonly List<JobDto> _jobs = new();

    private CloudEdgeBillingJobArgumentsFluidValidator _validator = null!;

    private const long ProcessId = 23346;

    protected override async Task OnInitializedAsync()
    {
        _validator = new CloudEdgeBillingJobArgumentsFluidValidator(DbContext);
        _releaseDto = await ReleasesApi.ReleasesGetByIdAsync(ProcessId);
        await base.OnInitializedAsync();
    }

    private async Task OnClick()
    {
        var process = _releaseDto ?? throw new ArgumentNullException(nameof(_releaseDto));
        
        await _form.Validate();

        if (_form.IsValid)
        {
            var startJobsRequest = new StartJobsRequest( new StartProcessDto
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
