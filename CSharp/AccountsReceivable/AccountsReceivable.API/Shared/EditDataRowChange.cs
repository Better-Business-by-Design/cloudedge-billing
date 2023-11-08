using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;

namespace AccountsReceivable.API.Shared;

public class EditDataRowChange : IDataRowChange
{
    public IDataRow OriginalDataRow { get; init; } = null!;
    public IDataRow CurrentDataRow { get; init; } = null!;

    public async Task ApplyChange(ApplicationDbContext dbContext)
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task RevertChange(ApplicationDbContext dbContext)
    {
        await dbContext.EditValue(OriginalDataRow);
    }
}