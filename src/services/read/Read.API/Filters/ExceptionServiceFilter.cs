using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Read.API.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Read.API.Filters
{
    public class ExceptionServiceFilter : IExceptionFilter
    {
        private readonly ApplicationInsights _applicationInsights;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExceptionServiceFilter(IOptions<ApplicationInsights> applicationInsights, IHostingEnvironment hostingEnvironment)
        {
            _applicationInsights = applicationInsights.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public async void OnException(ExceptionContext context)
        {
            var telemetry = new TelemetryClient();
            telemetry.InstrumentationKey = _applicationInsights.InstrumentKey;

            var time = DateTime.Now;
            var sw = Stopwatch.StartNew();
            telemetry.Context.Operation.Id = Guid.NewGuid().ToString();

            telemetry.TrackRequest("Read API", time, sw.Elapsed, HttpStatusCode.InternalServerError.ToString(), false);

            context.ExceptionHandled = true;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var model = new ProblemDetails
            {
                Title = "Internal server error",
                Detail = context.Exception.Message
            };

            if (!_hostingEnvironment.IsProduction())
            {
                model.Detail += $": {context.Exception.StackTrace}";
            }

            using (var writer = new StreamWriter(context.HttpContext.Response.Body))
            {
                new JsonSerializer().Serialize(writer, model);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }
    }
}
