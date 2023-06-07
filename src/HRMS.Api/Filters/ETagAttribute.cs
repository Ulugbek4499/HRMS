using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.Api.Filters
{
    public class ETagAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var originalBodyStream = context.HttpContext.Response.Body;
            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.HttpContext.Response.Body = responseBody;

                    await next();

                    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        var eTag = GenerateETag(responseBody);

                        context.HttpContext.Response.Headers.Add("ETag", eTag);

                        var requestETag = context.HttpContext.Request.Headers["If-None-Match"].FirstOrDefault();
                        if (requestETag == eTag)
                        {
                            context.Result = new StatusCodeResult((int)HttpStatusCode.NotModified);
                            return;
                        }
                    }

                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            finally
            {
                context.HttpContext.Response.Body = originalBodyStream;
            }
        }

        private string GenerateETag(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(stream);
                var eTag = Convert.ToBase64String(hashBytes);
                return eTag;
            }
        }
    }


}
