using HRMS.Application;
using HRMS.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Services.AddControllers();

builder.Services.AddResponseCaching();
builder.Services.AddOutputCache();
builder.Services.AddMemoryCache();
builder.Services.AddLazyCache();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.DisplayRequestDuration());
}

app.UseHttpsRedirection();

app.UseResponseCaching();
app.UseOutputCache();

app.UseFileServer();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

