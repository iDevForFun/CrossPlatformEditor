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
    }
}