using AccountsReceivable.BAL.Data;

namespace AccountsReceivable.API.Shared.DataRowChange;

public interface IDataRowChange
{ 
    Task ApplyChange(ApplicationDbContext dbContext);

    Task RevertChange(ApplicationDbContext dbContext);
}