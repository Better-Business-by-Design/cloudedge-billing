using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;

namespace AccountsReceivable.API.Shared;

public interface IDataRowChange
{ 
    Task ApplyChange(ApplicationDbContext dbContext);

    Task RevertChange(ApplicationDbContext dbContext);
}