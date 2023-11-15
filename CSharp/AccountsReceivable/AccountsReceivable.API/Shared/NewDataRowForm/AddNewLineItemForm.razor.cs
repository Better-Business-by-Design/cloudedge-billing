using System.Collections.Immutable;
using AccountsReceivable.API.Shared.NewDataRowForm;
using AccountsReceivable.BL.Models.Application;

namespace AccountsReceivable.API.Shared.NewDataRowForm;

public partial class AddNewLineItemForm : AddNewDataRowForm<LineItem>
{
    private ImmutableList<Account> _accounts;

    protected override async Task OnInitializedAsync()
    {
        _accounts = DbContext.Accounts.ToImmutableList();
        await base.OnInitializedAsync();
    }

}