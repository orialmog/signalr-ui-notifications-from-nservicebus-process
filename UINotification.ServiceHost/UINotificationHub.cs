using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using UINotifications.TypeContracts;

namespace UINotification.ServiceHost
{

    public class UINotificationHub : Hub
    {
        public static ConcurrentDictionary<string, UIStateEnum> UIState = new ConcurrentDictionary<string, UIStateEnum>();
        
        public string AssignState(string elementId, UIStateEnum? state)
        {

            if (state != null)
            {
                UIState[elementId] = state.Value;
            }
            else
            {
                state = UIState.ContainsKey(elementId) ? 
                    UIState[elementId] : UIStateEnum.Ready;
            }

            Console.WriteLine($"{Context.ConnectionId} connected for element {elementId}");

            var context = GlobalHost.ConnectionManager.GetHubContext<UINotificationHub>();
            if (state.Value == UIStateEnum.Busy)
            {

                UINotificationHub.UIState[elementId] = state.Value;
                context.Clients.All.Notify(elementId, state.Value.ToString());
            }

            return state.Value.ToString();
        }


    }
}