using UdpMessageSender;

namespace UdpMessageSender
{
	public class SupportConnectionRequestSent : UdpMessage
	{

		public SupportConnectionRequestSent(string targetAddress, int targetPort)
		{
			this.TargetAddress = targetAddress;
			this.TargetPort = targetPort;
		}

		public string TargetAddress { get; private set; }
		public int TargetPort { get; private set; }
	}
}