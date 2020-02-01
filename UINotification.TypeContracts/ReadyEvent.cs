namespace UINotifications.TypeContracts
{
    public class ReadyEvent : NotificationEvent
    {
        public ReadyEvent() : base( UIStateEnum.Ready)
        {
        }
    }
}