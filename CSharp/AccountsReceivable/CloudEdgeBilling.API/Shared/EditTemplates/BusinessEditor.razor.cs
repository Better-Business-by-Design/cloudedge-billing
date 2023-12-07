using CloudEdgeBilling.BL.Models.Application;
using CloudEdgeBilling.BL.Models.Enum;

namespace CloudEdgeBilling.API.Shared.EditTemplates;

/// <inheritdoc />
public partial class BusinessEditor : DataRowEditor<LineItem, Business>
{
    protected override Business Value => Context.Item.Business;

    protected override async Task HandleValueChanged(Business? newBusiness)
    {
        Context.Item.Business = newBusiness ?? throw new ArgumentNullException(nameof(newBusiness));
        await DataGrid.CommittedItemChanges.InvokeAsync(Context.Item);
    }

    protected override string GetPrintValue(Business? business)
    {
        return business?.Name ?? throw new ArgumentNullException(nameof(business));
    }
}

