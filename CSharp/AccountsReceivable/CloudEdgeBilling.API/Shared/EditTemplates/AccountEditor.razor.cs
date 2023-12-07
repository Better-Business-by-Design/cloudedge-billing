using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.EditTemplates;

/// <inheritdoc />
public partial class AccountEditor : DataRowEditor<LineItem, Account>
{
    protected override Account Value => Context.Item.Account;

    protected override async Task HandleValueChanged(Account? newAccount)
    {
        Context.Item.Account = newAccount ?? throw new ArgumentNullException(nameof(newAccount), 
            "Account field on Line Item is not nullable but received nullable value in account editor.");
        await DataGrid.CommittedItemChanges.InvokeAsync(Context.Item);
    }

    protected override string GetPrintValue(Account? value)
    {
        return value?.Name ?? throw new ArgumentNullException(nameof(value), 
            "Unable to get print value for non-nullable field Account on DTO LineItem.");
    }
}

