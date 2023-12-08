using CloudEdgeBilling.BL.Models.Application;
using CloudEdgeBilling.BL.Models.Enum;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Select;

/// <inheritdoc />
public partial class BusinessEditor : DataRowSelectEditor<LineItem, Business>
{
    protected override Business? Value
    {
        get => Context.Item.Business;
        set => Context.Item.Business = value ?? throw new ArgumentNullException(nameof(value));
    }

    protected override string GetPrintValue(Business? business)
    {
        return business?.Name ?? throw new ArgumentNullException(nameof(business));
    }
}

