namespace MiniStore.Infrastructure.Notifications
{
    public interface IPushNotifier
    {
        void SendPush(string deviceId, string message);
    }
}
