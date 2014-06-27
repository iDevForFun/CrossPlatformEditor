using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
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

        public async void ReportLoaded(string fileName, int clientId)
        {
            if (hubConnection.State == ConnectionState.Disconnected)
            {
                await hubConnection.Start();
            }

            hubProxy.Invoke<string>("LoadImage", fileName, clientId);
            Debug.WriteLine(string.Format("Image loaded: {0}", fileName));
        }

        public async void ReportFlip(int clientId)
        {
            if (hubConnection.State == ConnectionState.Disconnected)
            {
				await hubConnection.Start();
            }

			hubProxy.Invoke("SendFlip", clientId);
			Debug.WriteLine("Image flipped");
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