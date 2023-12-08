using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Select;

/// <inheritdoc />
public partial class PayMonthlyPlanEditor : DataRowSelectEditor<Customer, PayMonthlyPlan>
{
    protected override PayMonthlyPlan? Value
    {
        get => Context.Item.PayMonthlyPlan;
        set => Context.Item.PayMonthlyPlan = value;
    }

    protected override string GetPrintValue(PayMonthlyPlan? plan)
    {
        return plan?.PlanName ?? string.Empty;
    }
}