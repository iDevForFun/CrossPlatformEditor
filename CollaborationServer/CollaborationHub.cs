using CrossPlatformLogic.Network;
using Microsoft.AspNet.SignalR;

namespace CollaborationServer
{
    public class CollaborationHub : Hub
    {
        public void SendFlip(int clientId)
        {
            Clients.Others.broadcastFlip(new NetworkEvent
            {
                ClientId = clientId,
                Message = "flip",
                Type = EventType.Flip
            });
        }

		public void LoadImage(string fileName, int clientId)
		{
			Clients.Others.broadcastLoad(new NetworkEvent
				{
                    ClientId = clientId,
					Message = "loaded",
					Data = fileName,
					Type = EventType.Loaded
				});

		}

    }
}