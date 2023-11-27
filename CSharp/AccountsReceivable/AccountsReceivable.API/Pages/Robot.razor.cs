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
    [Inject] private ReleasesApi ReleasesApi { get; set; } = null!;
    [Inject] private JobsApi JobsApi { get; set; } = null!;
    
    [Inject] private ApplicationDbContext DbContext { get; set; } = null!;

    private MudForm form;
    private CloudEdgeBillingJobArguments _jobArguments;
    
    private ReleaseDto? _releaseDto;
    
    private DateTime? _yearMonth;

    private readonly List<JobDto> _jobs = new();

    private CloudEdgeBillingJobArgumentsFluidValidator Validator;

    private const long ProcessId = 23346;
    private const long FolderId = 314814;

    protected override async Task OnInitializedAsync()
    {
        Validator = new CloudEdgeBillingJobArgumentsFluidValidator(DbContext);
        _releaseDto = await ReleasesApi.ReleasesGetByIdAsync(ProcessId, xUipathOrganizationUnitId: FolderId);
        await base.OnInitializedAsync();
    }

    private async Task OnClick()
    {
        var process = _releaseDto ?? throw new ArgumentNullException(nameof(_releaseDto));
        
        await form.Validate();

        if (form.IsValid)
        {
            var startJobsRequest = new StartJobsRequest()
            {
                StartInfo = new StartProcessDto()
                {
                    ReleaseKey = process.Key,
                    Strategy = StartProcessDto.StrategyEnum.ModernJobsCount,
                    JobsCount = 1,
                    InputArguments = JsonConvert.SerializeObject(_jobArguments)
                }
            };
            var jobODataValue = await JobsApi.JobsStartJobsAsync(startJobsRequest, xUipathOrganizationUnitId: FolderId);
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
        public DateTime in_StartRangeDateTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
        

}
