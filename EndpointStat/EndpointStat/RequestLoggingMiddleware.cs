using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndpointStats
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private RequestCollection _collection;
        private readonly ICollectionInterface _collectionInterface;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration iconfig, ICollectionInterface collectionInterface)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
            _configuration = iconfig;
            _collection = new RequestCollection();
            _collectionInterface = collectionInterface;
        }
        public async Task Invoke(HttpContext context)
        {
            
            if (Convert.ToBoolean(_configuration.GetSection("EndpointStatsConfig:StatsCollectionEnable")))
            {
                if (_collection.CountAllEndPoint <= Convert.ToInt32(_configuration.GetSection("EndpointStatsConfig:StatsCollectionDepth")))
                {
                    await _next(context);
                    await _collectionInterface.StartCollection(context, _collection, _logger);
                }
                if (_collection.CountAllEndPoint == Convert.ToInt32(_configuration.GetSection("EndpointStatsConfig:StatsCollectionDepth")))
                {
                   await _collectionInterface.EndCollection(_collection,_logger);
                }
                
            }
        }
       
    }
}

