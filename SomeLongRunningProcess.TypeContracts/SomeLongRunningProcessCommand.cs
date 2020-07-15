using System;
using NServiceBus;
using UINotifications.TypeContracts;

namespace SomeLongRunningProcess.TypeContracts
{ 

    public class SomeLongRunningProcessCommand : UIElementUpdate, ICommand
    {
        public Guid MessageId { get; set; } 
    }
}