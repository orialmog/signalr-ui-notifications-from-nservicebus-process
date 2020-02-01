using System;
using NServiceBus;
using UINotifications.TypeContracts;

namespace SomeLongRunningProcess.TypeContracts
{


    public class SomeLongRunningProcessCommand : ElementUpdate , ICommand
    {
        public Guid MessageId { get; set; } 
    }
}