using System.Net;

namespace VlAutoUpdateClient;

public class VlHttpClientHandler : HttpClientHandler
{
    public VlHttpClientHandler()
    {
        //Set here whatever you need to get configured
        AutomaticDecompression = DecompressionMethods.All;
        MaxConnectionsPerServer = 1000;
        AllowAutoRedirect = false;
    }
}