using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public class AddDataRowChange : IDataRowChange
{
    public AddDataRowChange(IDataRow dataRow, ApplicationDbContext context)
    {
        _dataRow = dataRow;
        _context = context;
    }

    private readonly IDataRow _dataRow;
    private readonly ApplicationDbContext _context;

    public Task ApplyChange()
    {
        return _context.AddValue(_dataRow);
    }

    public async Task RevertChange()
    {
        await _context.RemoveValue(_dataRow);
        await _context.DisposeAsync();
    }

    public override string ToString()
    {
        return $"Add Data Row Change: {_dataRow}";
    }
}