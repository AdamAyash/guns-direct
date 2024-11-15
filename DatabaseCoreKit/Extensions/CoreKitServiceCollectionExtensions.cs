namespace DatabaseCoreKit.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    public static class CoreKitServiceCollectionExtensions
    {
        public static void AddCoreKitServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseConnectionPool, DatabaseConnectionPool>();
        }
    }
}
