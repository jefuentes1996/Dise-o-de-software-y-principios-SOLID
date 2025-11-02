namespace MiniStore.Infrastructure.Payments
{
    public interface IPaymentMethod
    {
        bool Charge(decimal amount);
    }
}
