using System;
using NServiceBus;

namespace UINotifications.TypeContracts
{
    public class NotificationEvent : IEvent
    {
        public NotificationEvent( UIStateEnum State)
        {
            MessageId = Guid.NewGuid();
            this.State = State;
        }

        public Guid MessageId { get; set; }
        public string UIElementId { get; set; }
        public UIStateEnum State { get; }


    }
}