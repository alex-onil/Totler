using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Trade_MVC5.Katana_Test
{
    public class KatanaMiddlewareTest
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public KatanaMiddlewareTest(RequestDelegate next, ILoggerFactory loggerFactory)
            {
             _next = next;
             _logger = loggerFactory.CreateLogger<KatanaMiddlewareTest>();
            }

        public async Task Invoke(HttpContext context)
        {
            // Debug.Write("Handling request: " + context.Request.Path);
            _logger.LogInformation("Handling request: " + context.Request.Path);
            await _next.Invoke(context);
            _logger.LogInformation("Finished handling request.");
            }

        }
    }
