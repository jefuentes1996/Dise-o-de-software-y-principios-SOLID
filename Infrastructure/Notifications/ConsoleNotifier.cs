using System;

namespace MiniStore.Infrastructure.Notifications
{
    public class ConsoleNotifier : IEmailNotifier, ISmsNotifier
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"[EMAIL] To:{to} Subj:{subject} Body:{body}");
        }

        public void SendSms(string phone, string message)
        {
            Console.WriteLine($"[SMS] To:{phone} Msg:{message}");
        }
    }
}
