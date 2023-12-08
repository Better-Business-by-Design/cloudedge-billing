using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Switch;

public partial class IsActiveEditor : DataRowSwitchEditor<Customer>
{
    protected override bool Value
    {
        get => Context.Item.IsActive;
        set => Context.Item.IsActive = value;
    }
}