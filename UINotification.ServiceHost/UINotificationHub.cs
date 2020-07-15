using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Transports;
using UINotifications.TypeContracts;

namespace UINotification.ServiceHost
{

    public class UINotificationHub : Hub
    {
        public static ConcurrentDictionary<string, UIStateEnum> UIState = new ConcurrentDictionary<string, UIStateEnum>();
        public static ConcurrentDictionary<string, List<string>> UIUsers = new ConcurrentDictionary<string, List<string>>();

        public string AssignState(string elementId, UIStateEnum? state = null)
        { 
            state = ReloadOrSetState(elementId, state);
            NotifyConnectedUsers(elementId, state);
            Console.WriteLine($"User: {Context.ConnectionId}, ElementId: {elementId}, State: {state}");
            return state.Value.ToString();
        }

        private UIStateEnum? ReloadOrSetState(string elementId, UIStateEnum? state)
        {
            if (state == null) // Reload of current state.
            {
                state = UIState.ContainsKey(elementId) ? UIState[elementId] : UIStateEnum.Ready;
            }
            else
            {
                UIState[elementId] = state.Value; //Set state.
            }
            return state;
        }

        private void NotifyConnectedUsers(string elementId, UIStateEnum? state)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UINotificationHub>();
            UIState[elementId] = state.Value;
            List<string> connectionIds = GetUpdateConnectionsForElement(elementId);
            foreach (var connectionId in connectionIds)
            {
                context.Clients.Client(connectionId).Notify(elementId, state.Value.ToString());
            }
        }

        private List<string> GetUpdateConnectionsForElement(string elementId)
        {
            var contextConnectionIds = UIUsers.ContainsKey(elementId) ? UIUsers[elementId] : new List<string>();
            contextConnectionIds.Add(Context.ConnectionId);
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            foreach (var connectionId in contextConnectionIds.ToArray())
            {
                var connectionAlive = heartBeat.GetConnections().FirstOrDefault(c => c.ConnectionId == connectionId);
                if (!connectionAlive.IsAlive) contextConnectionIds.Remove(connectionId);
            }
            UIUsers[elementId] = contextConnectionIds;
            return contextConnectionIds;
        }
    }
}