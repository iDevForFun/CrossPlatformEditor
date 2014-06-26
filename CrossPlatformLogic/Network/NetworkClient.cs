using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Microsoft.AspNet.SignalR.Client;

namespace CrossPlatformLogic.Network
{
    public class NetworkClient : INetworkClient
    {
        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        public NetworkClient()
        {
            hubConnection = new HubConnection("http://localhost:58396/");
            hubProxy = hubConnection.CreateHubProxy("CollaborationHub");
        }

        public async void ReportLoaded(string imagePath)
        {
            if (hubConnection.State == ConnectionState.Disconnected)
            {
                await hubConnection.Start();
            }
            Debug.WriteLine(string.Format("Image loaded: {0}", imagePath));
        }

        public async void ReportFlip()
        {
            if (hubConnection.State == ConnectionState.Disconnected)
            {
                await hubConnection.Start();
            }
            hubProxy.Invoke("SendFlip");
            Debug.WriteLine("Image fliped");
        }

        public IObservable<NetworkEvent> OnNetworkEvent()
        {
            return  Observable.Create<NetworkEvent>(observer => 
                Scheduler.Default.Schedule(() =>
            {
                hubProxy.On<NetworkEvent>("broadcastFlip", networkEvent =>
                {
                    observer.OnNext(networkEvent);
                });
            }));

        }
    }
}