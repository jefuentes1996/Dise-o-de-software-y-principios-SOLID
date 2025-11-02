using System;

namespace MiniStore.Infrastructure.Payments
{
    public class CryptoPayment : IPaymentMethod
    {
        public bool Charge(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("[CRYPTO] Monto inválido.");
                return false;
            }

            Console.WriteLine($"[CRYPTO] Transacción validada por {amount:C}");
            return true;
        }
    }
}
