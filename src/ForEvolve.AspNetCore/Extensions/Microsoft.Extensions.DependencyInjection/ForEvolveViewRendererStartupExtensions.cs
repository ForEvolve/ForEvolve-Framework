using Microsoft.Extensions.DependencyInjection.Extensions;
using ForEvolve.AspNetCore.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveViewRendererStartupExtensions
    {
        /// <summary>
        /// Adds the <c>IViewRenderer</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveViewRenderer(this IServiceCollection services)
        {
            services.TryAddScoped<IViewRenderer, ViewRenderer>();
            return services;
        }
    }
}