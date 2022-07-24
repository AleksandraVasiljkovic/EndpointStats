using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndpointStats
{
    public class StartEndCollection : ICollectionInterface
    {
        public async Task StartCollection(HttpContext context,RequestCollection collection, ILogger logger)
        {
            try
            {
                collection.Watch.Start();
                collection.CountAllEndPoint++;
                if (collection.Collection.ContainsKey(context.Request.Method + context.Request.Path.Value))
                {
                    collection.Collection[context.Request.Method + context.Request.Path.Value]++;
                }
                else
                {
                    collection.Collection.Add(context.Request.Method + context.Request.Path.Value, collection.CountByEndPoint);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }
        public async Task EndCollection(RequestCollection collection, ILogger logger)
        {
            collection.Watch.Stop();
            logger.LogInformation(
            "Endpoint stats : {maxENDP} called {maxValue} times in {0:00}.{1:00}",
            collection.Collection.Aggregate((l, r) => l.Value > r.Value ? l : r).Key,
            collection.Collection.Aggregate((l, r) => l.Value > r.Value ? l : r).Value,
            collection.Watch.Elapsed.Seconds, collection.Watch.Elapsed.Milliseconds / 10);
        }
    }
}
