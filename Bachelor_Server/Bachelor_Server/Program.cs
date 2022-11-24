using Bachelor_Server.BusinessLayer.Services.Account;
using Bachelor_Server.BusinessLayer.Services.Email;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.BusinessLayer.Services.ScheduleService;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.DataAccessLayer.Repositories.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.Schedule;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7086").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            policy.WithOrigins("https://bachelor.azurewebsites.net").AllowAnyHeader().AllowAnyMethod()
                .AllowCredentials();
        });
});

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<BachelorDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BachelorSQLDatabase"));
    }
);
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );


builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IWorkerConfigurationRepo, WorkerConfigurationRepo>();
builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ILogRepo, LogRepo>();
builder.Services.AddScoped<IEmailSerivce, EmailService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IWorkerConfigService, WorkerConfigService>();
builder.Services.AddScoped<IRestService, RestService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<Job>();
builder.Services.AddControllers();


var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();