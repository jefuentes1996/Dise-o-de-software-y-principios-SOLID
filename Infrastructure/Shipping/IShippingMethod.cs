namespace MiniStore.Infrastructure.Shipping
{
    public interface IShippingMethod
    {
        decimal CalculateCost(decimal totalWeightKg, decimal subtotal);
        void Ship(string address, decimal totalWeightKg);
    }
}
