﻿using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Read.API.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Read.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly ApplicationInsights _applicationInsights;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ErrorHandlerMiddleware(IOptions<ApplicationInsights> options, IHostingEnvironment hostingEnvironment)
        {
            _applicationInsights = options.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            var time = DateTime.Now;
            var sw = Stopwatch.StartNew();

            var telemetry = new TelemetryClient()
            {
                InstrumentationKey = _applicationInsights.InstrumentationKey
            };

            telemetry.Context.Operation.Id = Guid.NewGuid().ToString();

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (ex == null)
            {
                return;
            }

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.HttpContext.Request.Path,
                Detail = ex.Message
            };

            problemDetails.Title = "Server error";
            problemDetails.Status = StatusCodes.Status500InternalServerError;

            if (_hostingEnvironment.IsDevelopment())
            {
                problemDetails.Detail += ": " + ex.StackTrace;
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/json";

            using (var writer = new StreamWriter(context.Response.Body))
            {
                new JsonSerializer().Serialize(writer, problemDetails);
                await writer.FlushAsync();
            }

            telemetry.TrackRequest("Read API", time, sw.Elapsed, context.Response.StatusCode.ToString(), false);
        }
    }
}
