using System.Diagnostics;

namespace CrossPlatformLogic.Network
{
    public class NetworkClient: INetworkClient
    {
        public void ReportLoaded(string imagePath)
        {
            Debug.WriteLine(string.Format("Image loaded: {0}", imagePath));
        }

        public void ReportFlip()
        {
            Debug.WriteLine("Image fliped");
        }
    }
}
