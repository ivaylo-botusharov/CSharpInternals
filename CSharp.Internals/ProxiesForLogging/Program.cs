using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Castle.DynamicProxy;

using Clients;
using Interceptors;

ServiceProvider serviceProvider = ConfigureServices();

var customer = serviceProvider.GetRequiredService<ICustomer>();
bool isOrderSuccess = customer.PlaceOrder(123);
Console.WriteLine($"Order placed: {isOrderSuccess}");

customer.PerformTransaction();

var admin = serviceProvider.GetRequiredService<IAdmin>();
bool isUserAdded = admin.AddUser("John Doe");
Console.WriteLine($"User added: {isUserAdded}");

static ServiceProvider ConfigureServices()
{
    ServiceCollection services = new();

    services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
    });

    _ = services.AddSingleton<LoggingInterceptor>();
    _ = services.AddSingleton<IProxyGenerator, ProxyGenerator>();

    _ = services.AddTransient<Customer>();
    _ = services.AddTransient((IServiceProvider provider) =>
    {
        ICustomer customerProxy = BuildProxy<ICustomer, Customer, LoggingInterceptor>(provider);
        return customerProxy;
    });

    _ = services.AddTransient<IAdmin, Admin>();

    ServiceProvider serviceProvider = services.BuildServiceProvider();

    return serviceProvider;
}

static TInterface BuildProxy<TInterface, TBusinessClass, TInterceptor>(IServiceProvider provider)
    where TInterface: class
    where TBusinessClass : class, TInterface
    where TInterceptor : class, IInterceptor
{
    if (!typeof(TInterface).IsInterface)
    {
        throw new ArgumentException("TInterface must be an interface");
    }

    TBusinessClass businessObject = provider.GetRequiredService<TBusinessClass>();
    
    var proxyGenerator = provider.GetRequiredService<IProxyGenerator>();
    var interceptor = provider.GetRequiredService<TInterceptor>();

    TInterface businessObjectProxy = proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(
        businessObject,
        interceptor);

    return businessObjectProxy;
}

namespace Clients
{
    public interface ICustomer
    {
        bool PlaceOrder(int orderId);

        void PerformTransaction();
    }

    internal class Customer: ICustomer
    {
        public bool PlaceOrder(int orderId)
        {
            Console.WriteLine($"Placing order: {orderId}");
            return true;
        }

        public void PerformTransaction()
        {
            Console.WriteLine("Performing transaction");
        }
    }

    public interface IAdmin
    {
        bool AddUser(string userName);
    }

    public class Admin : IAdmin
    {
        public bool AddUser(string userName)
        {
            Console.WriteLine($"Adding user: {userName}");
            return true;
        }
    }
}

namespace Interceptors
{
    internal class LoggingInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            this.logger.LogInformation($"Entering {invocation.Method.Name}");
            invocation.Proceed();
            this.logger.LogInformation($"Exiting {invocation.Method.Name}");
        }
    }
}