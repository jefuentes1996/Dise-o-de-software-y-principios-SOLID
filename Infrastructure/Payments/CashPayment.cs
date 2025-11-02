using System;

namespace MiniStore.Infrastructure.Payments
{
    public class CashPayment : IPaymentMethod
    {
        public bool Charge(decimal amount)
        {
            Console.WriteLine($"[CASH] Pago en efectivo registrado por {amount:C}");
            return true;
        }
    }
}
