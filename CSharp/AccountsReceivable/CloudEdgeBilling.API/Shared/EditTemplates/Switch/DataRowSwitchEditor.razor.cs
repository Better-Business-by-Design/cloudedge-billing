using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Switch;

public abstract partial class DataRowSwitchEditor<TGrid> : DataRowEditor<TGrid, bool> where TGrid : IDataRow
{

    private async Task HandleSwitchValueChanged(bool? value)
    {
        var nonNullValue = value ?? throw new ApplicationException("Switch can't have a null value");
        await HandleValueChanged(nonNullValue);
    }
    
}

