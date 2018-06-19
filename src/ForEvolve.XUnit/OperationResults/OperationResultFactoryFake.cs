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
        private readonly IOperationResultFactory _instance;

        public OperationResultFactoryFake()
        {
            var services = new ServiceCollection();
            services
                .AddSingleton<IHostingEnvironment, HostingEnvironment>()
                .AddForEvolveOperationResults()
                .AddForEvolveErrorFactory(); // TODO: delete this line after update

            _instance = services
                .BuildServiceProvider()
                .GetService<IOperationResultFactory>();
        }

        public IOperationResult Create()
        {
            return _instance.Create();
        }

        public IOperationResult<TResult> Create<TResult>()
        {
            return _instance.Create<TResult>();
        }

        public IOperationResult<TResult> Create<TResult>(TResult result)
        {
            return _instance.Create(result);
        }
    }
}
