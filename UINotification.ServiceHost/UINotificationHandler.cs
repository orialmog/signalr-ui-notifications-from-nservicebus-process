using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using NServiceBus;
using UINotifications.TypeContracts;

namespace UINotification.ServiceHost
{
    public class UINotificationHandler :
        IHandleMessages<BusyMessage>,
        IHandleMessages<NotificationEvent>
    {
        public void Handle(NotificationEvent message)
        {

            var context = GlobalHost.ConnectionManager.GetHubContext<UINotificationHub>();
            if (message.UIElementId != string.Empty)
            {
                UINotificationHub.UIState[message.UIElementId] = message.State; 
                context.Clients.All.Notify(message.UIElementId, message.State.ToString());
                Console.WriteLine(
                    $"NotificationHandler -> Sent UINotification to ui {message.UIElementId} with {message.State}");
            }
            else
            {
                Console.WriteLine("NotificationHandler -> No one to hear " + message.UIElementId);
            }

        }

        public void Handle(BusyMessage message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UINotificationHub>();
            if (message.UIElementId != string.Empty)
            {
                UINotificationHub.UIState[message.UIElementId] = UIStateEnum.Busy;
                context.Clients.All.Notify(message.UIElementId, UIStateEnum.Busy.ToString());
                Console.WriteLine(
                    $"NotificationHandler -> Sent UINotification to ui {message.UIElementId} with {UIStateEnum.Busy}");
            }
            else
            {
                Console.WriteLine("NotificationHandler -> No one to hear " + message.UIElementId);
            }

        }
    }

}