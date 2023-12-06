using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;

namespace AccountsReceivable.API.Shared.DataRowChange;

public class EditDataRowChange : IDataRowChange
{

    public IDataRow OriginalDataRow { get; init; } = null!;
    public IDataRow DataRow { get; init; } = null!;

    
    
    public async Task ApplyChange(ApplicationDbContext dbContext)
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task RevertChange(ApplicationDbContext dbContext)
    {
        await dbContext.EditValue(DataRow, OriginalDataRow);
    }

    public override string ToString()
    {
        return DataRow.ToString() ?? base.ToString() ?? string.Empty;
    }
}