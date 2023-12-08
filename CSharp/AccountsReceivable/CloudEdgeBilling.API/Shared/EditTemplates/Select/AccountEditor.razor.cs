using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Select;

/// <inheritdoc />
public partial class AccountEditor : DataRowSelectEditor<LineItem, Account>
{
    protected override Account? Value
    {
        get => Context.Item.Account;
        set => Context.Item.Account = value ?? throw new ArgumentNullException(nameof(value),
            "Account field on Line Item is not nullable but received nullable value in account editor.");
    }

    protected override string GetPrintValue(Account? value)
    {
        return value?.Name ?? throw new ArgumentNullException(nameof(value), 
            "Unable to get print value for non-nullable field Account on DTO LineItem.");
    }
}

