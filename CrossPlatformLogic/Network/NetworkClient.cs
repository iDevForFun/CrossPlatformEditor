using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace CrossPlatformLogic.Network
{
    public class NetworkClient : INetworkClient
    {
        public void ReportLoaded(string imagePath)
        {
            Debug.WriteLine(string.Format("Image loaded: {0}", imagePath));
        }

        public void ReportFlip()
        {
            Debug.WriteLine("Image fliped");
        }

        public IObservable<NetworkEvent> OnNetworkEvent()
        {
            var onNetworkEvent = Observable.Create<NetworkEvent>(observer =>
            {
                observer.OnNext(new NetworkEvent
                {
                    Type = EventType.Loaded
                });
                return Scheduler.Default.Schedule(() =>
                {
                    int i = 0;
                    for (;;)
                    {
                        Thread.Sleep(1000); // here we do the long lasting background operation
                        i++;
                        var networkEvent = new NetworkEvent
                        {
                            Type = EventType.Flip
                        };
                        observer.OnNext(networkEvent);
                    }
                });
            });
            return onNetworkEvent;
        }
    }
}