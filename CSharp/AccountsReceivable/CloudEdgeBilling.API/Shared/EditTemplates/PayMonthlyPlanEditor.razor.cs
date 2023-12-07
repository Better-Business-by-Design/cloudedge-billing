using CloudEdgeBilling.API.Shared.EditTemplates;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.EditTemplates;

/// <inheritdoc />
public partial class PayMonthlyPlanEditor : DataRowEditor<Customer, PayMonthlyPlan>
{
    protected override PayMonthlyPlan? Value => Context.Item.PayMonthlyPlan;

    protected override async Task HandleValueChanged(PayMonthlyPlan? newPlan)
    {
        Context.Item.PayMonthlyPlan = newPlan;
        await DataGrid.CommittedItemChanges.InvokeAsync(Context.Item);
    }

    protected override string GetPrintValue(PayMonthlyPlan? plan)
    {
        return plan?.PlanName ?? string.Empty;
    }
}