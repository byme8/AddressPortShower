using System;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UdpMessageSender
{
	public class UdpMessager : IDisposable
	{
		//private ISubject<UdpMessage> messages;

		//IObservable<UdpMessage> Messages
		//	=> this.messages;

		private static JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			Formatting = Formatting.Indented
		};
		private Subject<UdpMessage> messages;

		public IObservable<UdpMessage> Messages
			=> this.messages.AsObservable();

		private UdpClient UdpClient
		{
			get;
			set;
		}

		public UdpMessager(int port = 0)
		{
			//this.messages = new Subject<UdpMessage>();
			this.UdpClient = new UdpClient(port);
			this.messages = new Subject<UdpMessage>();

			StartListeningAsync();
		}

		private async void StartListeningAsync()
		{
			while (this.UdpClient != null)
			{
				try
				{
					var request = await this.UdpClient.ReceiveAsync();
					var json = Encoding.UTF8.GetString(request.Buffer);
					var message = JsonConvert.DeserializeObject<UdpMessage>(json, Settings);

					message.FromAddress = request.RemoteEndPoint.Address.ToString();
					message.FromPort = request.RemoteEndPoint.Port;

					Console.WriteLine("Recieve: \n" + json);
					this.messages.OnNext(message);
				}
				catch (Exception ex)
				{
					this.messages.OnError(ex);
				}
			}
		}

		public async Task SendMessageAsync(UdpMessage message, string host, int port)
		{
			message.ToAddress = host;
			message.ToPort = port;
			var json = JsonConvert.SerializeObject(message, Settings);
			var bytes = Encoding.UTF8.GetBytes(json);

			await this.UdpClient.SendAsync(bytes, bytes.Length, host, port);

			Console.WriteLine("Send: \n" + json);
		}

		public void Dispose()
		{
			this.UdpClient?.Dispose();
			this.UdpClient = null;

			this.messages?.Dispose();
			this.messages = null;
		}
	}
}
