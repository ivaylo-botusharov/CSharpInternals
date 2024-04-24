using System.Runtime.CompilerServices;

var customer = new Customer();
customer.PlaceOrder(1); // invokes OrderInterceptor() and prints "Order interceptor: 1"
customer.PlaceOrder(2); // invokes PlaceOrder() and prints "Order placed: 2"

class Customer
{
    public void PlaceOrder(int orderId)
    {
        Console.WriteLine($"Order placed: {orderId}");
    }
}

namespace Sales
{
    static class OrderExtensions
    {
        [InterceptsLocation("Program.cs", line: 5, character: 10)]
        public static void OrderInterceptor(this Customer customer, int orderId)
        {
            Console.WriteLine($"Order interceptor: {orderId}");
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class InterceptsLocationAttribute: Attribute
    {
        public InterceptsLocationAttribute(string filePath, int line, int character)
        {
            _ = filePath;
            _ = line;
            _ = character;
        }
    }
}
