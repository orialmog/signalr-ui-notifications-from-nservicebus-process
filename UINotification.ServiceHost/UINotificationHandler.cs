using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using NServiceBus;
using UINotifications.TypeContracts;

namespace UINotification.ServiceHost
{
    public class UINotificationHandler :
        IHandleMessages<NotificationEvent>
    {
        public void Handle(NotificationEvent message)
        {
            if (message.UIElementId != string.Empty)
            {
                UINotificationHub.UIState[message.UIElementId] = message.State;
                var context = GlobalHost.ConnectionManager.GetHubContext<UINotificationHub>();
                var connectionIds = UINotificationHub.UIUsers.ContainsKey(message.UIElementId) ? UINotificationHub.UIUsers[message.UIElementId] : new List<string>();
                var hasData = message.GetType().GetInterfaces().Any(x =>  x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHaveData<>));
                var data = hasData ? ((dynamic) message).Data : null;
                foreach (var connectionId in connectionIds.ToArray())
                {
                    context.Clients.Client(connectionId).Notify(message.UIElementId, message.State.ToString(), data);
                }
                Console.WriteLine($"NotificationHandler -> Sent UINotification to ui {message.UIElementId} with {message.State}");
            }
            else
            {
                Console.WriteLine("NotificationHandler -> No one to hear " + message.UIElementId);
            }

        }





    }

}