using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;

namespace AccountsReceivable.API.Shared.DataRowChange;

public class RemoveDataRowsChange : IDataRowChange
{
    private IEnumerable<IDataRow> DataRowChanges { get; }

    public RemoveDataRowsChange(IEnumerable<IDataRow> dataRowChanges)
    {
        DataRowChanges = dataRowChanges;
    }

    public async Task ApplyChange(ApplicationDbContext dbContext)
    {
        await dbContext.RemoveValues(DataRowChanges);
    }

    public async Task RevertChange(ApplicationDbContext dbContext)
    {
        await dbContext.AddValues(DataRowChanges, false);
    }
    
    
}