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
    ServiceCollection services = new();

    services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
    });

    _ = services.AddTransient<IProxyGenerator, ProxyGenerator>();
    _ = services.AddTransient<LoggingInterceptor>();

    _ = services.AddTransient((IServiceProvider provider) =>
    {
        ICustomer customerProxy = BuildProxy<ICustomer, Customer, LoggingInterceptor>(provider);
        return customerProxy;
    });


    ServiceProvider serviceProvider = services.BuildServiceProvider();

    return serviceProvider;
}

static TInterface BuildProxy<TInterface, TBusinessClass, TInterceptor>(IServiceProvider provider)
    where TInterface: class
    where TBusinessClass : class, TInterface, new()
    where TInterceptor : class, IInterceptor
{
    if (!typeof(TInterface).IsInterface)
    {
        throw new ArgumentException("TInterface must be an interface");
    }

    TBusinessClass businessObject = new();

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
        void PlaceOrder(int orderId);
    }

    internal class Customer: ICustomer
    {
        public void PlaceOrder(int orderId)
        {
            Console.WriteLine($"Placing order: {orderId}");
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