using System.Net;
using System.Text.RegularExpressions;

namespace Soly.Utilities.Common;
public static class NetworkUtil
{
    public static IPAddress ParseIPAddress(string value)
    {
        if (string.Compare(value, "any", StringComparison.OrdinalIgnoreCase) == 0)
        {
            return IPAddress.Any;
        }
        else if (string.Compare(value, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
        {
            return IPAddress.Loopback;
        }
        else if (string.Compare(value, "auto", StringComparison.OrdinalIgnoreCase) == 0)
        {
            IPHostEntry iphe;
            iphe = Dns.GetHostEntry(Dns.GetHostName());
            return iphe.AddressList[0];
        }
        else if (Regex.IsMatch(value, "^([0-9]{1,3}\\.){3}[0-9]{1,3}$"))
        {
            return IPAddress.Parse(value);
        }
        else
        {
            IPHostEntry iphe;
            iphe = Dns.GetHostEntry(value);
            return iphe.AddressList[0];
        }
        throw new ArgumentException("Could not parse IP Address", nameof(value));
    }
    public static IPEndPoint GetIPEndPoint(string host, int port)
    {
        IPAddress ipAddress = ParseIPAddress(host);
        IPEndPoint ipEndpoint = new(ipAddress, port);
        return ipEndpoint;
    }
}