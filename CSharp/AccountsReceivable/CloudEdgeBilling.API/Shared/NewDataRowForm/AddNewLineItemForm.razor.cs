using System.Collections.Immutable;
using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;

namespace CloudEdgeBilling.API.Shared.NewDataRowForm;

public partial class AddNewLineItemForm : AddNewDataRowForm<LineItem>
{
    [Parameter] public ImmutableList<Account> Accounts { get; set; }
}