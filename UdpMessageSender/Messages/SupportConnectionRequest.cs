using UdpMessageSender;

namespace UdpMessageSender
{
	public class SupportConnectionRequest : UdpMessage
	{
		public SupportConnectionRequest(string targetAddress, int targetPort)
		{
			this.TargetAddress = targetAddress;
			this.TargetPort = targetPort;
		}

		public int TargetPort { get; private set; }
		public string TargetAddress { get; private set; }
		public int TargetFromPort { get; set; }
		public string TargetFromAddress { get; set; }
	}
}