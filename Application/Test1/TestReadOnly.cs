using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Test1
{
    internal class TestReadOnly
    {
        private static readonly HttpClient client = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (HttpRequestMessage message, X509Certificate2? certificate2, X509Chain? arg3, SslPolicyErrors arg4) => true,
            UseProxy = false,
            AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate)
        })
        {
            Timeout = TimeSpan.FromSeconds(20.0)
        };
         public static (double oldtimeout, double newtimeout) test(int timeout)
        {
            double oldtimeout = client.Timeout.TotalMilliseconds;
            client.Timeout = TimeSpan.FromMilliseconds(timeout);
            double newtimeout = client.Timeout.TotalMilliseconds;
            return (oldtimeout, newtimeout);

        }
    }
}
