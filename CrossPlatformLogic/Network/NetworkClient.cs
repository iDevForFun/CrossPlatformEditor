using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;


namespace CrossPlatformLogic.Network
{
    public class NetworkClient : INetworkClient
    {
        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        public NetworkClient()
        {
			hubConnection = new HubConnection("http://10.211.55.5/CollaborationServer/");
            hubProxy = hubConnection.CreateHubProxy("CollaborationHub");
        }

        public async void ReportLoaded(string fileName)
        {
            await EnsureConnectionState();
            hubProxy.Invoke<string>("LoadImage", fileName);
            Debug.WriteLine(string.Format("Image loaded: {0}", fileName));
        }

        public async void ReportFlip()
        {
            await EnsureConnectionState();
			hubProxy.Invoke("SendFlip");
			Debug.WriteLine("Image flipped");
        }

        public async void ReportLock()
        {
            await EnsureConnectionState();
            hubProxy.Invoke("SendLock");
            Debug.WriteLine("editor mode");
        }

        private async Task EnsureConnectionState()
        {
            if (hubConnection.State == ConnectionState.Disconnected)
            {
                await hubConnection.Start();
            }
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

                hubProxy.On<NetworkEvent>("broadcastLoad", networkEvent =>
                {
                    observer.OnNext(networkEvent);
                });
            }));
        }
    }
}