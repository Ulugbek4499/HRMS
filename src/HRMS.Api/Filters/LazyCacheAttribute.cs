using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace HRMS.Api.Filters
{
    public class LazyCacheAttribute : ActionFilterAttribute
    {
        private static IAppCache _cache;
        private string _Key;
        private readonly int _slidingTime;
        private readonly int _absoluteExpirationRelativeToNow;

        public LazyCacheAttribute(int slidingTime, int absoluteExpirationRelativeToNow)
        {
            _slidingTime = slidingTime;
            _absoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();
            _Key = context.HttpContext.Request.Path;

            bool _isCached = _cache.TryGetValue(_Key, out IActionResult val);

            if (!_isCached)
                await base.OnActionExecutionAsync(context, next);

            else
            {
                context.Result = val;
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_absoluteExpirationRelativeToNow),
                SlidingExpiration = TimeSpan.FromSeconds(_slidingTime)
            };

            _cache.Add(_Key, context.Result, options);
            base.OnActionExecuted(context);
        }

    }
    /* public class LazyCacheAttribute : ActionFilterAttribute
     {
         private static IAppCache _cache;
         private string _Key;
         private readonly int _slidingTime;
         private readonly int _absoluteExpirationRelativeToNow;

         public LazyCacheAttribute(int slidingTime, int absoluteExpirationRelativeToNow)
         {
             _slidingTime = slidingTime;
             _absoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
         }

         public override async Task OnActionExecutionAsync(
             ActionExecutingContext context, 
             ActionExecutionDelegate next)
         {
             _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();
             _Key = context.HttpContext.Request.Path;

             var res = await _cache.GetOrAddAsync(_Key, c =>
             {
                 c.SlidingExpiration = TimeSpan.FromSeconds(_slidingTime);
                 c.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_absoluteExpirationRelativeToNow);

                 return next();
             });

             if (res != null)
             {
                 context.Result = res.Result;
             }
         }
     }*/
}
