using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

partial class TransitDialog : ComponentBase
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;
    
    [Parameter]
    public string DocumentId { get; set; } = null!;
    
    [Parameter]
    public ICollection<Transit> Transits { get; set; } = null!;

    private HashSet<Transit> SelectedItems { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitializedAsync();

        SelectedItems = Transits
            .Where(transit => transit.DocumentId != null && transit.DocumentId.Equals(DocumentId))
            .ToHashSet();
    }
    
    private void SelectedItemsChanged()
    {
        foreach(var transit in Transits)
            if (transit.DocumentId != null && transit.DocumentId.Equals(DocumentId) && !SelectedItems.Contains(transit))
                transit.DocumentId = null;
            else if (SelectedItems.Contains(transit))
                transit.DocumentId = DocumentId;
    }
    
    private void Save()
    {
        MudDialog.Close();
    }
}