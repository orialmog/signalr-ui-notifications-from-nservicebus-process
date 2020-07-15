namespace UINotifications.TypeContracts
{
    public interface IHaveData<T>
    {
        T Data { get; set; }
    }
}