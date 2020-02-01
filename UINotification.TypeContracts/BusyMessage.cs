using System;
using NServiceBus;

namespace UINotifications.TypeContracts
{
    public class BusyMessage : IMessage
    {
        public string UIElementId { get; set; }
        public Guid MessageId { get; set; }
    }
}