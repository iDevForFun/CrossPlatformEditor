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

		public void SendRotate()
		{
			Clients.Others.broadcastRotate(new NetworkEvent
				{
					Message = "rotate",
					Type = EventType.Rotate
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

		public void SendLock(bool editing)
        {
            Clients.Others.broadcastLock(new NetworkEvent
            {
                Message = "lock",
					Data = editing.ToString(),
                Type = EventType.Lock
            });
        }
    }
}