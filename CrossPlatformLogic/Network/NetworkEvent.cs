namespace CrossPlatformLogic.Network
{
    public class NetworkEvent
    {
        public int ClientId { get; set; }
        public string Message { get; set; }
        public EventType Type { get; set; }
		public string Data { get; set; }
    }
}
