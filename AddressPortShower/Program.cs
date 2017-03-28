using System;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Text;
using Newtonsoft.Json;
using UdpMessageSender;

namespace AddressPortShower
{
	class Program
	{
		static void Main(string[] args)
		{
			Launch();
			Console.ReadKey();
		}

		private static async void Launch()
		{
			var udpMessager = new UdpMessager(5000);

			udpMessager.Messages.
				OfType<CurrentAddressRequest>().
				Subscribe(async message =>
				{
					await udpMessager.SendMessageAsync(
						new CurrentAddressResponce(), 
						message.FromAddress, 
						message.FromPort);
				});
		}
	}
}