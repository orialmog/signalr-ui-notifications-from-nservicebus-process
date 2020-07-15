namespace UINotifications.TypeContracts
{

    public class BusyEvent : NotificationEvent
    {
        public BusyEvent() : base(UIStateEnum.Busy)
        {
        }

    }
}