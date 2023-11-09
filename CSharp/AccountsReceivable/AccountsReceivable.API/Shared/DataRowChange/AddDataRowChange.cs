using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;

namespace AccountsReceivable.API.Shared;

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