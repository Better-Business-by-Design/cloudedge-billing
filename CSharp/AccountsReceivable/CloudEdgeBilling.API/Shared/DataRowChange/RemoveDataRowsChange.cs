using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public class RemoveDataRowsChange : IDataRowChange
{

    private ApplicationDbContext _context;
    
    public RemoveDataRowsChange(IEnumerable<IDataRow> dataRowChanges, ApplicationDbContext context)
    {
        DataRowChanges = dataRowChanges;
        _context = context;
    }

    private IEnumerable<IDataRow> DataRowChanges { get; }

    public async Task ApplyChange()
    {
        await _context.RemoveValues(DataRowChanges);
    }

    public async Task RevertChange()
    {
        await _context.AddValues(DataRowChanges, false);
        await _context.DisposeAsync();
    }
}