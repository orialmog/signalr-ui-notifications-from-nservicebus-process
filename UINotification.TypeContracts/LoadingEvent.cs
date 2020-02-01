namespace UINotifications.TypeContracts
{
    public class LoadingEvent : NotificationEvent
    {
        public LoadingEvent() : base(UIStateEnum.Loading)
        {
        }
    }
}