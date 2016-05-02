using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CSRedis
{
	/// <summary>
	/// Utils for endpoint.
	/// </summary>
	public class EndpointUtil
	{
		private readonly static Regex IpRegex = new Regex("[0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]+");


		/// <summary>
		/// On Mono DnsEndpoint does not work properly, so we are forced to use IEndPoint here.
		/// </summary>
		/// <param name="host">Ip or DNS of the host</param>
		/// <param name="port">Port of the endpoint.</param>
		/// <returns>IPEndPoint.</returns>
		public static IPEndPoint fromHostAndPort(String host, int port)
		{
			// If it's an IpAddress we are using simple parse.
			if (IpRegex.Match(host).Success)
			{
				return new IPEndPoint(IPAddress.Parse(host), port);
			}
			else
			{
				return new IPEndPoint(GetIPAddressInternal(host), port);
			}			
		}


		private static IPAddress GetIPAddressInternal(string hostNameOrAddress)
		{
			var addresses = Dns.GetHostAddresses(hostNameOrAddress);
			if (addresses.Length == 0)
			{
				throw new ArgumentException(
					"Unable to retrieve address from specified host name '" + hostNameOrAddress + "'",
					"hostNameOrAddress"
				);
			}
			return addresses.First();
		}

	}
}
