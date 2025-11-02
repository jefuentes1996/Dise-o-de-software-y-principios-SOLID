using System;
using MiniStore.Application.UseCases;
using MiniStore.Infrastructure.Notifications;

namespace MiniStoreWorkshopAntiPatterns
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MiniStore - Refactorizado SOLID";
            Console.WriteLine("=== MiniStore Refactorizado (Principios SOLID) ===\n");

            // 🔹 Inyección manual de dependencias
            var emailNotifier = new ConsoleNotifier();
            var smsNotifier = new ConsoleNotifier();

            var orderProcessor = new OrderProcessor(emailNotifier, smsNotifier);

            while (true)
            {
                Console.WriteLine("\n=== MENÚ ===");
                Console.WriteLine("1) Listar productos");
                Console.WriteLine("2) Registrar cliente");
                Console.WriteLine("3) Crear pedido y pagar");
                Console.WriteLine("4) Listar pedidos");
                Console.WriteLine("0) Salir");
                Console.Write("> ");

                var op = Console.ReadLine();

                if (op == "0")
                {
                    Console.WriteLine("\nGracias por usar MiniStore. ¡Hasta pronto!");
                    break;
                }

                switch (op)
                {
                    case "1":
                        orderProcessor.ListProducts();
                        break;

                    case "2":
                        Console.Write("Nombre: ");
                        var name = Console.ReadLine() ?? "";
                        Console.Write("Email: ");
                        var email = Console.ReadLine() ?? "";
                        Console.Write("Teléfono: ");
                        var phone = Console.ReadLine() ?? "";
                        orderProcessor.RegisterCustomer(name, email, phone);
                        break;

                    case "3":
                        Console.Write("Id Cliente: ");
                        var cid = Console.ReadLine() ?? "";
                        Console.Write("Sku Producto: ");
                        var sku = Console.ReadLine() ?? "";
                        Console.Write("Cantidad: ");
                        int.TryParse(Console.ReadLine(), out var qty);
                        Console.Write("Promo (standard/bf/vip/employee): ");
                        var promo = Console.ReadLine() ?? "";
                        Console.Write("Pago (card/cash/crypto): ");
                        var payment = Console.ReadLine() ?? "";
                        Console.Write("Envío (standard/express/drone): ");
                        var shipping = Console.ReadLine() ?? "";

                        orderProcessor.CreateOrderAndPay(cid, sku, qty, promo, payment, shipping);
                        break;

                    case "4":
                        orderProcessor.ListOrders();
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        break;
                }
            }
        }
    }
}
