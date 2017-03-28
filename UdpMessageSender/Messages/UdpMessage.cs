namespace UdpMessageSender
{
	public class UdpMessage
	{
		public string FromAddress { get; set; }
		public int FromPort { get; set; }

		public string ToAddress { get; set; }
		public int ToPort { get; set; }
	}
}