using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace AccountsReceivable.BAL.Extensions;

public static class ScheduleExtensions
{
    public static async Task ProcessDocuments(this Schedule baseSchedule, ApplicationDbContext dbContext)
    {
        var schedule = await dbContext.Schedules
            .Include(entity => entity.Prices)
            .Include(entity => entity.Uplifts)
            .SingleAsync(entity => entity.Id == baseSchedule.Id);

        if (schedule.StatusId is not (StatusId.Approved or StatusId.Overridden))
            throw new NotImplementedException();
        
        var documents = await dbContext.Documents
            .Include(entity => entity.Plant)
            .Include(entity => entity.Animals!)
            .ThenInclude(entity => entity.Grade)
            .Where(entity =>
                schedule.MeatworkName.Equals(entity.Plant.MeatworkName) &&
                schedule.StartDate <= entity.DateProcessed &&
                schedule.EndDate >= entity.DateProcessed
            )
            .ToListAsync();
        
        foreach (var document in documents)
            await document.CalculatePricesAsync(dbContext, schedule);
    }
}