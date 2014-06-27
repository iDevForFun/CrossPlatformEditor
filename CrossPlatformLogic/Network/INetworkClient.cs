﻿using System;
using System.Reactive;
using System.Threading;

namespace CrossPlatformLogic.Network
{
    public interface INetworkClient
    {
        void ReportLoaded(string imagePath);
        void ReportFlip();
        IObservable<NetworkEvent> OnNetworkEvent();
        void ReportLock();
    }
}
