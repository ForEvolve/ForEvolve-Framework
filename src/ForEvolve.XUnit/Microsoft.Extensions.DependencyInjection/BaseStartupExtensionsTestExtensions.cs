using Moq;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BaseStartupExtensionsTestExtensions
    {
        public static IServiceCollection AddSingletonMock<TService>(this IServiceCollection services, Action<Mock<TService>> setup = null)
            where TService : class
        {
            var serviceMock = new Mock<TService>();
            setup?.Invoke(serviceMock);
            services.AddSingleton(serviceMock.Object);
            return services;
        }
    }
}
