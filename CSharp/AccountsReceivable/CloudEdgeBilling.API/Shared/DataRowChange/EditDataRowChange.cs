using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public class EditDataRowChange : IDataRowChange
{
    private readonly ApplicationDbContext _context;
    
    private readonly IDataRow _originalDataRow;
    private readonly IDataRow _dataRow;

    public EditDataRowChange(IDataRow originalDataRow, IDataRow dataRow, ApplicationDbContext context)
    {
        _originalDataRow = originalDataRow;
        _dataRow = dataRow;
        _context = context;
    }

    public async Task ApplyChange()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RevertChange()
    {
        await _context.EditValue(_dataRow, _originalDataRow);
        await _context.DisposeAsync();
    }

    public override string ToString()
    {
        return _dataRow.ToString() ?? base.ToString() ?? string.Empty;
    }
}