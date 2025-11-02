using System;

namespace MiniStore.Infrastructure.Payments
{
    public class CardPayment : IPaymentMethod
    {
        public bool Charge(decimal amount)
        {
            Console.WriteLine($"[CARD] Pago con tarjeta realizado por {amount:C}");
            return true;
        }
    }
}
