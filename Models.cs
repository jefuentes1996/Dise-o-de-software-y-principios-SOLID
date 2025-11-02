namespace MiniStore.Domain
{
    public class Product
    {
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal WeightKg { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class OrderLine
    {
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal WeightKg { get; set; }
    }

    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string CustomerId { get; set; } = string.Empty;
        public List<OrderLine> Lines { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string ShippingType { get; set; } = string.Empty;
        public bool Paid { get; set; }
    }
}
