using NServiceBus;

namespace UINotifications.TypeContracts
{
    public class UIElementUpdate : ICommand
    {
        public string UIElementId { get; set; }
    }
}