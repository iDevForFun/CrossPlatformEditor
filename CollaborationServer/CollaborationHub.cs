using CrossPlatformLogic.Network;
using Microsoft.AspNet.SignalR;

namespace CollaborationServer
{
    public class CollaborationHub : Hub
    {
        public void SendFlip()
        {
            Clients.Others.broadcastFlip(new NetworkEvent
            {
                Message = "flip",
                Type = EventType.Flip
            });
        }

		public void LoadImage(string fileName)
		{
			Clients.Others.broadcastLoad (new NetworkEvent {
				Message = "loaded",
				Data = fileName,
				Type = EventType.Loaded
			});
		}

        public void SendLock()
        {
            Clients.Others.broadcastLoad(new NetworkEvent
            {
                Message = "lock",
                Data = string.Empty,
                Type = EventType.Lock
            });
        }
    }
}