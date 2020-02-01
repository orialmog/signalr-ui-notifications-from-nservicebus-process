using System;
using Microsoft.Owin.Hosting;
using NServiceBus;

namespace UINotification.ServiceHost
{
    public class SignlarBootstrapper : IWantToRunWhenBusStartsAndStops
    {
        private IDisposable disposable;

        public void Start()
        {
           disposable =  WebApp.Start<Startup>("http://localhost:8080/");
        }

        public void Stop()
        {
            disposable.Dispose();
        }
    }
}