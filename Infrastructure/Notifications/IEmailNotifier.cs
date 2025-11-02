namespace MiniStore.Infrastructure.Notifications
{
    public interface IEmailNotifier
    {
        void SendEmail(string to, string subject, string body);
    }
}
