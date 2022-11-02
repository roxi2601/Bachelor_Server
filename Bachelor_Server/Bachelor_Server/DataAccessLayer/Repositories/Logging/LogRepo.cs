using Bachelor_Server.DataAccessLayer.Repositories.Logging.Helper;
using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bachelor_Server.DataAccessLayer.Repositories.Logging;

public class LogRepo : ILogRepo
{
    private BachelorDBContext dbContext;
    public LogRepo(BachelorDBContext bachelorDBContext)
    {
        dbContext = bachelorDBContext;
    }
    public async Task AddErrorLog(string description, string exception, DateTime date)
    {
        await using (dbContext)
        {
            Log error = new Log()
            {
                Description = description,
                StackTrace = exception,
                Date = date.ToString(),
                FkWorkerId = 0
            };
            dbContext.Logs.Add(error);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task AddLog(string content, DateTime date)
    {
        await using (dbContext)
        {
            Log log = new Log()
            {
                Description = content,
                StackTrace = "",
                Date = date.ToString(),
                FkWorkerId = 0
            };
            dbContext.Logs.Add(log);
            await dbContext.SaveChangesAsync();
        }
    }
}