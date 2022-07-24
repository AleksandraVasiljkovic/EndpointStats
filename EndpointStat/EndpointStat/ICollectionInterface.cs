
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EndpointStats
{
    public interface ICollectionInterface
    {
        Task StartCollection(HttpContext context, RequestCollection collection, ILogger logger);
        Task EndCollection(RequestCollection collection, ILogger logger);
    }
}
