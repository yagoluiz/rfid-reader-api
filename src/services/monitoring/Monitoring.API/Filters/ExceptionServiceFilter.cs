using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Monitoring.API.Common;
using Monitoring.CrossCutting.Settings;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;

namespace Monitoring.API.Filters
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
            telemetry.InstrumentationKey = _applicationInsights.InstrumentationKey;

            var time = DateTime.Now;
            var sw = Stopwatch.StartNew();
            telemetry.Context.Operation.Id = Guid.NewGuid().ToString();

            telemetry.TrackRequest("Monitoring API", time, sw.Elapsed, HttpStatusCode.InternalServerError.ToString(), false);

            context.ExceptionHandled = true;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (_hostingEnvironment.IsDevelopment() || _hostingEnvironment.IsStaging())
            {
                var erros = new string[] { $"{context.Exception.Message}/{context.Exception.StackTrace}" };
                var response = new ResponseCommon(null, "Processing error",
                    new ResponseErrorCommon(erros.Length, erros));

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            else
            {
                var erros = new string[] { "Processing error" };
                var response = new ResponseCommon(null, "Processing error",
                    new ResponseErrorCommon(erros.Length, erros));

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
