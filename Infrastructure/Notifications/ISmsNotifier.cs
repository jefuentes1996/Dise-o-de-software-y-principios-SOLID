namespace MiniStore.Infrastructure.Notifications
{
    public interface ISmsNotifier
    {
        void SendSms(string phone, string message);
    }
}
