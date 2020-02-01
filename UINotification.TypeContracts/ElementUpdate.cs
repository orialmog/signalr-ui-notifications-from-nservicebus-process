using NServiceBus;

namespace UINotifications.TypeContracts
{
    public class ElementUpdate : ICommand
    {
      public string UIElementId { get; set; } 
    }
}