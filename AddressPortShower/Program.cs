using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace AddressPortShower
{
	class Program
	{
		class DeviceAddress
		{
			public DeviceAddress(IPEndPoint endPoint)
			{
				this.Address = endPoint.Address.ToString();
				this.Port = endPoint.Port;
			}

			public int Port { get; private set; }
			public string Address { get; private set; }
		}

		static void Main(string[] args)
		{
			Launch();
			Console.ReadKey();
		}

		private static async void Launch()
		{
			var udpClient = new UdpClient(5000);

			while (true)
			{
				var request = await udpClient.ReceiveAsync();
				var message = JsonConvert.SerializeObject(
					new DeviceAddress(request.RemoteEndPoint));

				var bytes = Encoding.UTF8.GetBytes(message);

				udpClient.SendAsync(bytes, bytes.Length, request.RemoteEndPoint);
			}
		}
	}
}