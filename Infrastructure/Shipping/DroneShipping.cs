using System;

namespace MiniStore.Infrastructure.Shipping
{
    public class DroneShipping : IShippingMethod
    {
        public decimal CalculateCost(decimal totalWeightKg, decimal subtotal)
        {
            
            return totalWeightKg <= 2 ? 15 : 40;
        }

        public void Ship(string address, decimal totalWeightKg)
        {
            if (totalWeightKg > 2)
            {
                Console.WriteLine("[SHIP] El peso excede el límite de dron, se requiere método alternativo.");
                return;
            }

            Console.WriteLine($"[SHIP] Envío por dron a {address} ({totalWeightKg} kg)");
        }
    }
}
