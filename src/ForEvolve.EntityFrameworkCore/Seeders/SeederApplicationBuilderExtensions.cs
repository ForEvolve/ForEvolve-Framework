using ForEvolve.EntityFrameworkCore.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class SeederApplicationBuilderExtensions
    {
        /// <summary>
        /// Find the <see cref="ISeederManager{TDbContext}"/> and call its Seed method,
        /// executing all the <see cref="ISeeder{TDbContext}"/> that were registered.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder Seed<TDbContext>(this IApplicationBuilder app)
            where TDbContext : DbContext
        {
            var factory = app.ApplicationServices.GetService<ISeederManagerFactory>();
            var manager = factory.Create<TDbContext>();
            manager.Seed();
            return app;
        }
    }
}
