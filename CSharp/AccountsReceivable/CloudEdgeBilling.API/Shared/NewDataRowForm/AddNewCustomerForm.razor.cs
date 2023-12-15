using System.Collections.Immutable;
using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared.NewDataRowForm;

/// <inheritdoc />
public partial class AddNewCustomerForm : AddNewDataRowForm<Customer>
{
    [Parameter] public required ImmutableList<PayMonthlyPlan> PayMonthlyPlans { get; set; }

    private readonly PatternMask _guidMask = new("********-****-****-****-************");
}