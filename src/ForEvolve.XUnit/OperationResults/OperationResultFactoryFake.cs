using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.ErrorFactory.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.OperationResults
{
    public class OperationResultFactoryFake : IOperationResultFactory
    {
        public IOperationResultFactory Instance { get; }

        public OperationResultFactoryFake()
        {
            var services = new ServiceCollection();
            services
                .AddSingleton<IHostingEnvironment, HostingEnvironment>()
                .AddForEvolveOperationResults();

            Instance = services
                .BuildServiceProvider()
                .GetService<IOperationResultFactory>();
        }

        public IOperationResult Create()
        {
            return Instance.Create();
        }

        public IOperationResult<TResult> Create<TResult>()
        {
            return Instance.Create<TResult>();
        }

        public IOperationResult<TResult> Create<TResult>(TResult result)
        {
            return Instance.Create(result);
        }
    }
}
