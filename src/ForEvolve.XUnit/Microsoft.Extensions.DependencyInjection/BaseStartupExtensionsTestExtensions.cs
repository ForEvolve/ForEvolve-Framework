using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BaseStartupExtensionsTestExtensions
    {
        public static IServiceCollection AddSingletonMock<TService>(this IServiceCollection services)
            where TService : class
        {
            var serviceMock = new Mock<TService>();
            services.AddSingleton(serviceMock.Object);
            return services;
        }
    }
}
