using Bachelor_Server.DataAccessLayer.Repositories.Logging.Helper;
using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bachelor_Server.DataAccessLayer.Repositories.Logging;

public class LogRepo : ILogRepo
{
    private IDbContextFactory<BachelorDBContext> _dbContext;
    public LogRepo(IDbContextFactory<BachelorDBContext> bachelorDBContext)
    {
        _dbContext = bachelorDBContext;
    }
    // public async Task AddErrorLog(string description, string exception, DateTime date)
    // {
    //     await using (var context = await _dbContext.CreateDbContextAsync())
    //     {
    //         Log error = new Log()
    //         {
    //             Description = description,
    //             StackTrace = exception,
    //             Date = date.ToString()
    //         };
    //         await context.Logs.AddAsync(error);
    //         await context.SaveChangesAsync();
    //     }
    // }

   //public async Task AddLog(string content, DateTime date)
   public async Task AddLog(JsonMessage message)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            Log log = new Log()
            {
                //Status code?? TODO:
                Description = message.Description,
                StackTrace = message.Exception,
                Date = message.Date.ToString()
            };
            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
        }
    }
}