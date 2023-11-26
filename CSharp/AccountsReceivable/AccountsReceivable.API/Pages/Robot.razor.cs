using AccountsReceivable.API.Shared.FluidValidation;
using AccountsReceivable.API.Shared.UiPath;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

public partial class Robot : ComponentBase
{
    [Inject] private UiPathClient UiPathClient { get; set; } = null!;
    [Inject] private ApplicationDbContext DbContext { get; set; } = null!;

    private MudForm form;
    
    private UiPathProcessDto? _processDto;
    private string? _argument;
    
    private DateTime? _yearMonth;

    private readonly List<UiPathJobDto> _jobs = new();
    
    private CloudEdgeBillingJobArguments _jobArguments = new();

    private CloudEdgeBillingJobArgumentsFluidValidator Validator;

    protected override async Task OnInitializedAsync()
    {
        Validator = new CloudEdgeBillingJobArgumentsFluidValidator(DbContext);
        var processes = await UiPathClient.GetProcessesByName("CloudEdgeBilling");
        _processDto = processes.FirstOrDefault();
        await base.OnInitializedAsync();
    }

    private async Task OnClick()
    {
        var process = _processDto ?? throw new ArgumentNullException(nameof(_processDto));
        
        await form.Validate();

        if (form.IsValid)
        {
            var startInfo = UiPathJobStartInfoFactory.BuildJobStartInfo(process, _jobArguments);
            var job = await UiPathClient.StartProcess(startInfo);
            _jobs.Add(job);
            
            StateHasChanged();
        }
    }

    private void RemoveJob(UiPathJobDto job)
    {
        _jobs.Remove(job);
        StateHasChanged();
    }

}
