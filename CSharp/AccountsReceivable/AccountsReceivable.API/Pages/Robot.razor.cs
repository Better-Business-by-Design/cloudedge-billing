using System.Globalization;
using AccountsReceivable.API.Shared.UiPath;
using AccountsReceivable.BL.Models.Json;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AccountsReceivable.API.Pages;

public partial class Robot : ComponentBase
{
    [Inject] private UiPathClient UiPathClient { get; set; } = null!;
    
    private UiPathProcessDto? _processDto;
    private string? _argument;
    
    private DateTime? _yearMonth;

    private readonly List<UiPathJobDto> _jobs = new();

    protected override async Task OnInitializedAsync()
    {
        var processes = await UiPathClient.GetProcessesByName("CloudEdgeBilling");
        _processDto = processes.FirstOrDefault();
        _argument = _processDto?.Arguments.GetInputJArray().FirstOrDefault()?.Value<string>("name");
        await base.OnInitializedAsync();
    }

    private async Task OnClick()
    {
        var process = _processDto ?? throw new ArgumentNullException(nameof(_processDto));
        var argument = _argument ?? throw new ArgumentNullException(nameof(_argument));
        
        var startInfo = UiPathJobStartInfoFactory.BuildJobStartInfo(
            process.Key,
            inputArguments:  new Dictionary<string, string>()
            {
                {argument, (_yearMonth ?? DateTime.Today.AddDays(-1 * (DateTime.Today.Day - 1))).ToString(CultureInfo.InvariantCulture)}
            }
        );

        var job = await UiPathClient.StartProcess(startInfo);
        _jobs.Add(job);
        StateHasChanged();
    }

}
