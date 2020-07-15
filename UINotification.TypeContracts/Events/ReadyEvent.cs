namespace UINotifications.TypeContracts
{
    public class ReadyEvent : NotificationEvent, IHaveData<int>
    {
        public ReadyEvent() : base( UIStateEnum.Ready)
        {
        }

        public int Data { get; set; }
    }
}