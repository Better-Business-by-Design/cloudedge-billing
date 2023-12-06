using CloudEdgeBilling.BAL.Data;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public interface IDataRowChange
{ 
    Task ApplyChange(ApplicationDbContext dbContext);

    Task RevertChange(ApplicationDbContext dbContext);
}