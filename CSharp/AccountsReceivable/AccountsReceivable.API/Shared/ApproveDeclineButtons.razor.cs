using AccountsReceivable.BL.Models.Enum;
using System.Security.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

partial class ApproveDeclineButtons : ComponentBase
{
    [Parameter]
    public RoleId? Role { get; set; }

    [Parameter]
    public StatusId? Status { get; set; }

    [Parameter]
    public Func<Task>? CommentTask { get; set; }
    
    [Parameter]
    public Func<Task>? TransitTask { get; set; }

    [Parameter]
    public bool TransitRequired { get; set; } = false;

    [Parameter]
    public Func<Task>? RecalculateTask { get; set; }

    [Parameter]
    public Func<Task>? SetApprovedTask { get; set; }

    [Parameter]
    public Func<Task>? SetDeclinedTask { get; set; }

    [Parameter]
    public string? DetailObjectType { get; set; }

    [Inject]
    protected virtual ILogger<ApproveDeclineButtons> Logger { get; set; } = default!;
        
    [Inject] 
    protected virtual IDialogService DialogService { get; set; } = default!;

    private async void InvokeCommentTask()
    {
        await CommentTask?.Invoke()!;
    }

    private async void RecalculateButton()
    {
        await RecalculateTask?.Invoke()!;
    }

    private async void OpenApproveDialog()
    {
        var result = await OpenAuthRequiredDialog("approve");
        if (result) await SetApprovedTask?.Invoke()!;
    }

    private async void OpenDeclineDialog()
    {
        var result = await OpenAuthRequiredDialog("decline");
        if (result) await SetDeclinedTask?.Invoke()!;
    }

    private async Task<bool> OpenAuthRequiredDialog(string changeString)
    {
        if ((Role ?? RoleId.None) is not (RoleId.ReadWrite or RoleId.Administrator))
        {
            throw new AuthenticationException($"User with role {(Role ?? RoleId.None).ToString()} doesn't have permission to change Status values, needs either {RoleId.ReadWrite.ToString()} or {RoleId.Administrator.ToString()}");
        }

        var result = await DialogService.ShowMessageBox(
            "Approval Confirmation",
            "Are you sure you want to " + changeString + " this " + (DetailObjectType ?? "Object") + "?",
            changeString, cancelText: "Cancel");

        return result ?? false;
    }

    private bool IsStatusNotPendingOrDeclined()
    {
        return (Status ?? StatusId.None) is not (StatusId.Pending or StatusId.Declined);
    }

    private bool IsStatusNotPending()
    {
        return (Status ?? StatusId.None) is not StatusId.Pending;
    }
    
    private bool IsStatusApproved()
    {
        return (Status ?? StatusId.None) is StatusId.Approved;
    }

}