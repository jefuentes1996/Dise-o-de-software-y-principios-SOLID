using System;

namespace MiniStore.Infrastructure.Shipping
{
    public class StandardShipping : IShippingMethod
    {
        public decimal CalculateCost(decimal totalWeightKg, decimal subtotal)
        {
            return totalWeightKg <= 5 ? 25 : 50;
        }

        public void Ship(string address, decimal totalWeightKg)
        {
            Console.WriteLine($"[SHIP] Envío estándar a {address} ({totalWeightKg} kg)");
        }
    }
}
