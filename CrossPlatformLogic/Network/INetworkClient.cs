using System;
using System.Reactive;

namespace CrossPlatformLogic.Network
{
    public interface INetworkClient
    {
        void ReportLoaded(string imagePath);
        void ReportFlip();
        IObservable<NetworkEvent> OnNetworkEvent();
    }
}
