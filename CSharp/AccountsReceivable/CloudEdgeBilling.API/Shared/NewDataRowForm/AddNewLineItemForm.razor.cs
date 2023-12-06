using System.Collections.Immutable;
using AccountsReceivable.API.Shared.NewDataRowForm;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Shared.NewDataRowForm;

public partial class AddNewLineItemForm : AddNewDataRowForm<LineItem>
{
    [Parameter]
    public ImmutableList<Account> Accounts { get; set; }
    
}