﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MakeHttpRequestFeature.Handlers
{
    public class RequestDataHandler: DelegatingHandler
    {
        private readonly ILogger<RequestDataHandler> _logger;

        private const string RequestSourceHeaderName = "Request-Source";
        private const string RequestSource = "HttpClientFactorySampleApp";
        private const string RequestIdHeaderName = "Request-Identifier";

        public RequestDataHandler(ILogger<RequestDataHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var identifier = Guid.NewGuid(); // some information we want to generate and add per request

            _logger.LogInformation("Starting request {Identifier}", identifier);

            request.Headers.Add(RequestSourceHeaderName, RequestSource);
            request.Headers.Add(RequestIdHeaderName, identifier.ToString());

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
