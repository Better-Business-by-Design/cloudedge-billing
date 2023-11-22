using MudBlazor;

namespace AccountsReceivable.BL.Models.Json.Enum;

public static class Extensions
{
    public static Severity Severity(this UiPathJobState state)
    {
        return state switch
        {
            UiPathJobState.Pending => MudBlazor.Severity.Info,
            UiPathJobState.Running => MudBlazor.Severity.Normal,
            UiPathJobState.Successful => MudBlazor.Severity.Success,
            UiPathJobState.Faulted => MudBlazor.Severity.Error,
            UiPathJobState.Stopping => MudBlazor.Severity.Warning,
            UiPathJobState.Terminating => MudBlazor.Severity.Warning,
            UiPathJobState.Suspended => MudBlazor.Severity.Warning,
            UiPathJobState.Resumed => MudBlazor.Severity.Normal,
            UiPathJobState.Stopped => MudBlazor.Severity.Error,
            _ => throw new NotImplementedException($"{state} not implemented in Severity extension method")
        };
    }

    public static bool Completed(this UiPathJobState state)
    {
        return state is UiPathJobState.Successful or UiPathJobState.Stopped or UiPathJobState.Faulted;
    }
}
public enum UiPathJobState
{
    Pending,
    Running,
    Successful,
    Faulted,
    Stopping,
    Terminating,
    Suspended,
    Resumed,
    Stopped
}