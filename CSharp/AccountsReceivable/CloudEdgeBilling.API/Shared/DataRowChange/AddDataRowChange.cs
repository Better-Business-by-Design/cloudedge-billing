using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public class AddDataRowChange : IDataRowChange
{
    public AddDataRowChange(IDataRow dataRow)
    {
        DataRow = dataRow;
    }

    private IDataRow DataRow { get; }

    public async Task ApplyChange(ApplicationDbContext dbContext)
    {
        await dbContext.AddValue(DataRow);
    }

    public async Task RevertChange(ApplicationDbContext dbContext)
    {
        await dbContext.RemoveValue(DataRow);
    }
}