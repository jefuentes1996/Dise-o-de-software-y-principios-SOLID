using System;

namespace MiniStore.Infrastructure.Shipping
{
    public class ExpressShipping : IShippingMethod
    {
        public decimal CalculateCost(decimal totalWeightKg, decimal subtotal)
        {
            return 60; // tarifa fija simple
        }

        public void Ship(string address, decimal totalWeightKg)
        {
            Console.WriteLine($"[SHIP] Envío exprés a {address} ({totalWeightKg} kg)");
        }
    }
}
