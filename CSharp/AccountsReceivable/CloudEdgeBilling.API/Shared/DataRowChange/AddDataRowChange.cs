using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public class AddDataRowChange : IDataRowChange 
{
    
    private IDataRow DataRow { get; } 

    public AddDataRowChange(IDataRow dataRow)
    {
        DataRow = dataRow;
    }
    
    public async Task ApplyChange(ApplicationDbContext dbContext)
    {
        await dbContext.AddValue(DataRow);
    }

    public async Task RevertChange(ApplicationDbContext dbContext)
    {
        await dbContext.RemoveValue(DataRow);
    }
}