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
            Debug.WriteLine("report loaded");
        }

        public void ReportFlip()
        {
            Debug.WriteLine("ReportFlip");
        }

        public void ReportRotate()
        {
            Debug.WriteLine("ReportRotate");
        }

        public IObservable<NetworkEvent> OnNetworkEvent()
        {
            Debug.WriteLine("OnNetworkEvent");
            return Observable.Empty<NetworkEvent>();
        }

        public void ReportLock(bool editing)
        {
            Debug.WriteLine("ReportLock");
        }
    }
}