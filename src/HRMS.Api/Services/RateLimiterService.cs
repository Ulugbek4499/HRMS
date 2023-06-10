using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace HRMS.Api.Services
{
    public static class RateLimiterService
    {
    public static IServiceCollection AddRateLimiterService(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddTokenBucketLimiter("TokenBucket", options =>
            {

                options.TokenLimit = 10;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(15);
                options.TokensPerPeriod = 5;
                options.QueueLimit = 3;

            });

            options.AddSlidingWindowLimiter("SlidingLimiter", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.QueueLimit = 10;
                options.PermitLimit = 15;
                options.AutoReplenishment = true;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.SegmentsPerWindow = 4;

            });

            options.AddFixedWindowLimiter("Api", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.AutoReplenishment = true;
                options.PermitLimit = 10;
                options.QueueLimit = 5;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

            });

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(),
                    partition =>
                    new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        AutoReplenishment = true,
                        Window = TimeSpan.FromSeconds(10),
                    });
            });

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            };
        });

        return services;
    }

    }
}
