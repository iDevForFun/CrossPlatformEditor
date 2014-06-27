using System;
using System.Diagnostics;
using System.Reactive.Linq;
using CrossPlatformLogic.Network;

namespace WindowsEditor.Wpf.DevMode
{
    internal class DebugNetworkClient : INetworkClient
    {
        public void ReportLoaded(string imagePath)
        {
            Debug.Write("report loaded");
        }

        public void ReportFlip()
        {
            Debug.Write("report loaded");
        }

        public void ReportRotate()
        {
            Debug.Write("report loaded");
        }

        public IObservable<NetworkEvent> OnNetworkEvent()
        {
            Debug.Write("report loaded");
            return Observable.Empty<NetworkEvent>();
        }

        public void ReportLock()
        {
            Debug.Write("report loaded");
        }
    }
}