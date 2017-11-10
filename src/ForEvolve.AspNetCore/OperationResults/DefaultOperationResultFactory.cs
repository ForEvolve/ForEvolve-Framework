using System;

namespace ForEvolve.AspNetCore
{
    public class DefaultOperationResultFactory : IOperationResultFactory
    {
        private readonly IErrorFactory _errorFactory;

        public DefaultOperationResultFactory(IErrorFactory errorFactory)
        {
            _errorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
        }

        public IOperationResult Create()
        {
            return new DefaultOperationResult(_errorFactory);
        }

        public IOperationResult<TResult> Create<TResult>(TResult result)
        {
            return new DefaultOperationResultWithResult<TResult>(_errorFactory)
            {
                Result = result
            };
        }

        public IOperationResult<TResult> Create<TResult>()
        {
            return new DefaultOperationResultWithResult<TResult>(_errorFactory);
        }
    }
}
