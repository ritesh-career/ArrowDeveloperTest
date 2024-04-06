using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Arrow.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ConsoleRunner>();
                    services.AddTransient<IAccountDataStore, AccountDataStore>();
                    services.AddTransient<IAccountService, AccountService>();
                    services.AddTransient<IPaymentService, PaymentService>();
                    services.AddTransient<IPaymentSchemeValidatorFactory, PaymentSchemeValidatorFactory>();
                    services.AddTransient<IPaymentSchemeValidator, BacsValidator>();
                    services.AddTransient<IPaymentSchemeValidator, ChapsValidator>();
                    services.AddTransient<IPaymentSchemeValidator, FasterPaymentsValidator>();
                    services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("ArrowDeveloperTest"));
                })
                .Build();

            var svc = ActivatorUtilities.CreateInstance<ConsoleRunner>(host.Services);
            svc.Run();
        }
    }
}
