using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Castle.DynamicProxy;

using Clients;
using Interceptors;

ServiceProvider serviceProvider = ConfigureServices();

var customer = serviceProvider.GetRequiredService<ICustomer>();
customer.PlaceOrder(123);

static ServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
    });

    services.AddTransient<IProxyGenerator, ProxyGenerator>();
    services.AddTransient<LoggingInterceptor>();

    services.AddTransient<ICustomer>(provider => 
    {
        var customer = new Customer();
        var loggingInterceptor = provider.GetRequiredService<LoggingInterceptor>();
        
        var proxyGenerator = provider.GetRequiredService<IProxyGenerator>();

        ICustomer customerProxy = proxyGenerator.CreateInterfaceProxyWithTarget<ICustomer>(
            customer,
            loggingInterceptor);

        return customerProxy;
    });


    ServiceProvider serviceProvider = services.BuildServiceProvider();

    return serviceProvider;
}

namespace Clients
{
    public interface ICustomer
    {
        void PlaceOrder(int orderId);
    }

    public class Customer: ICustomer
    {
        public void PlaceOrder(int orderId)
        {
            Console.WriteLine($"Placing order: {orderId}");
        }
    }
}

namespace Interceptors
{
    public class LoggingInterceptor : IInterceptor
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