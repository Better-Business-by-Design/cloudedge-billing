using System.Collections.Immutable;
using CloudEdgeBilling.BL.Models.Application;
using CloudEdgeBilling.BL.Models.Enum;
using Microsoft.AspNetCore.Components;

namespace CloudEdgeBilling.API.Shared.NewDataRowForm;

/// <inheritdoc />
public partial class AddNewLineItemForm : AddNewDataRowForm<LineItem>
{
    [Parameter] public ImmutableList<Account> Accounts { get; set; } = null!;

    [Parameter] public ImmutableList<Business> Businesses { get; set; } = null!;
}