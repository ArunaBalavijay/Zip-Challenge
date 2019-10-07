using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Zip.Challenge.Common.Exceptions
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        try
                        {
                            var exception = contextFeature.Error;
                            var logger = app.ApplicationServices.GetService(typeof(ILogger<IApplicationBuilder>)) as ILogger;
                            logger?.LogError(exception, exception.Message);
                        }
                        catch (Exception)
                        {

                        }

                        await context.Response.WriteAsync(new ErrorDetail
                        {
                            Status = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
