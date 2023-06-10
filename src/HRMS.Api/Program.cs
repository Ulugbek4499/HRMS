using System.Threading.RateLimiting;
using HRMS.Api.Services;
using HRMS.Application;
using HRMS.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

public class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var builder = WebApplication.CreateBuilder(args);
        SerilogService.SerilogSettings(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("RedisDB");
        });

        builder.Services.AddEndpointsApiExplorer();
       // builder.Services.AddSwaggerGen();
        builder.Services.AddApplicationService();
        builder.Services.AddRateLimiterService();
        builder.Services.AddInfrastructureService(builder.Configuration);
        builder.Services.AddLazyCache();


        var app = builder.Build();
        app.UseRateLimiter();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.DisplayRequestDuration());
        }

        app.UseHttpsRedirection();
        app.UseFileServer();
        app.UseStaticFiles();
        app.UseDefaultFiles();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}