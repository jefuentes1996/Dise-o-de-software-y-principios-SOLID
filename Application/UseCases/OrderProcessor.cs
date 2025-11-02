using System;
using System.Collections.Generic;
using System.Linq;
using MiniStore.Domain;
using MiniStore.Infrastructure.Notifications;
using MiniStore.Infrastructure.Payments;
using MiniStore.Infrastructure.Shipping;

namespace MiniStore.Application.UseCases
{
    public class OrderProcessor
    {
        private readonly IEmailNotifier _emailNotifier;
        private readonly ISmsNotifier _smsNotifier;

        private readonly List<Product> _products = new();
        private readonly List<Customer> _customers = new();
        private readonly List<Order> _orders = new();

        public OrderProcessor(IEmailNotifier emailNotifier, ISmsNotifier smsNotifier)
        {
            _emailNotifier = emailNotifier;
            _smsNotifier = smsNotifier;
            SeedProducts();
        }

        private void SeedProducts()
        {
            _products.AddRange(new[]
            {
                new Product {Sku="P1", Name="Mouse", Price=100m, WeightKg=0.2m},
                new Product {Sku="P2", Name="Teclado", Price=300m, WeightKg=0.8m},
                new Product {Sku="P3", Name="Laptop", Price=6000m, WeightKg=2.5m},
            });
        }

        public void ListProducts()
        {
            Console.WriteLine("\n=== PRODUCTOS DISPONIBLES ===");
            foreach (var p in _products)
                Console.WriteLine($"{p.Sku} - {p.Name} - {p.Price:C} - {p.WeightKg}kg");
        }

        public void RegisterCustomer(string name, string email, string phone)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = name,
                Email = email,
                Phone = phone
            };

            _customers.Add(customer);
            Console.WriteLine($"Cliente registrado: {customer.Name} ({customer.Id})");
        }

        public void CreateOrderAndPay(string customerId, string sku, int qty, string promo, string payment, string shipping)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            var product = _products.FirstOrDefault(p => p.Sku == sku);

            if (customer == null || product == null)
            {
                Console.WriteLine("Cliente o producto no encontrado.");
                return;
            }

            var order = new Order
            {
                Id = Guid.NewGuid().ToString("N"),
                CustomerId = customer.Id
            };

            order.Lines.Add(new OrderLine
            {
                Sku = product.Sku,
                Quantity = qty,
                UnitPrice = product.Price,
                WeightKg = product.WeightKg
            });

            order.Subtotal = order.Lines.Sum(l => l.UnitPrice * l.Quantity);

            order.Subtotal = ApplyPromotion(promo, order.Subtotal);

            var shippingMethod = GetShippingMethod(shipping);
            var paymentMethod = GetPaymentMethod(payment);

            var totalWeight = order.Lines.Sum(l => l.WeightKg * l.Quantity);
            order.ShippingCost = shippingMethod.CalculateCost(totalWeight, order.Subtotal);
            order.Total = order.Subtotal + order.ShippingCost;

            var paid = paymentMethod.Charge(order.Total);
            if (!paid)
            {
                Console.WriteLine("Pago rechazado.");
                return;
            }

            shippingMethod.Ship("Zona 10, Ciudad de Guatemala", totalWeight);
            order.Paid = true;

            _orders.Add(order);

            _emailNotifier.SendEmail(customer.Email, "Pedido confirmado", $"Su pedido {order.Id} fue procesado correctamente. Total: {order.Total:C}");
            _smsNotifier.SendSms(customer.Phone, $"Pedido {order.Id} confirmado por {order.Total:C}");

            Console.WriteLine($"✅ Pedido {order.Id} creado y pagado. Total: {order.Total:C}");
        }

        private decimal ApplyPromotion(string promo, decimal subtotal)
        {
            return promo switch
            {
                "bf" => subtotal * 0.7m,
                "vip" => subtotal * 0.85m,
                "employee" => subtotal * 0.5m,
                _ => subtotal
            };
        }

        private IShippingMethod GetShippingMethod(string shipping)
        {
            return shipping switch
            {
                "express" => new ExpressShipping(),
                "drone" => new DroneShipping(),
                _ => new StandardShipping()
            };
        }

        private IPaymentMethod GetPaymentMethod(string payment)
        {
            return payment switch
            {
                "card" => new CardPayment(),
                "crypto" => new CryptoPayment(),
                _ => new CashPayment()
            };
        }

        public void ListOrders()
        {
            Console.WriteLine("\n=== PEDIDOS REGISTRADOS ===");
            foreach (var o in _orders)
                Console.WriteLine($"{o.Id} - Cliente:{o.CustomerId} - Total:{o.Total:C} - Pagado:{o.Paid}");
        }
    }
}
