using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public class RemoveDataRowsChange : IDataRowChange
{
    public RemoveDataRowsChange(IEnumerable<IDataRow> dataRowChanges)
    {
        DataRowChanges = dataRowChanges;
    }

    private IEnumerable<IDataRow> DataRowChanges { get; }

    public async Task ApplyChange(ApplicationDbContext dbContext)
    {
        await dbContext.RemoveValues(DataRowChanges);
    }

    public async Task RevertChange(ApplicationDbContext dbContext)
    {
        await dbContext.AddValues(DataRowChanges, false);
    }
}