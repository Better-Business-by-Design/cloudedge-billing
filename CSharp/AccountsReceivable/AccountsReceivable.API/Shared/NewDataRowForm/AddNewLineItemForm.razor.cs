using System.Collections.Immutable;
using AccountsReceivable.API.Shared.NewDataRowForm;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;

namespace AccountsReceivable.API.Shared.NewDataRowForm;

public partial class AddNewLineItemForm : AddNewDataRowForm<LineItem>
{
    [CascadingParameter]
    protected ImmutableList<Account> Accounts { get; set; }
}