using System.Collections.Generic;

namespace EndpointStats
{
    public class RequestCollection
    {
        
        public Dictionary<string, int> Collection { get; set; }
        public System.Diagnostics.Stopwatch Watch { get; set; }
        public int CountAllEndPoint { get; set; }
        public int CountByEndPoint { get; set; }
        public RequestCollection()
        {
            Collection = new Dictionary<string, int>();
            Watch = new System.Diagnostics.Stopwatch();
            CountAllEndPoint = 0;
            CountByEndPoint = 1;
        }
    }
}
