using System;
using System.Reactive;

namespace CrossPlatformLogic.Network
{
    public interface INetworkClient
    {
        void ReportLoaded(string imagePath, int clientId);
        void ReportFlip(int clientId);
       IObservable<NetworkEvent> OnNetworkEvent();
    }
}
