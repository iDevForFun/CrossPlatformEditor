using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformLogic.Network
{
    public interface INetworkClient
    {
        void ReportLoaded(string imagePath);
        void ReportFlip();
    }
}
