﻿using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
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
            await document.CalculatePrices(dbContext, schedule);
    }
}