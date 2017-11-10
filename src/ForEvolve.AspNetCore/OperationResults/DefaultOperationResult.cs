using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    public class DefaultOperationResult : IOperationResult
    {
        private readonly List<Error> _errors;
        private readonly List<Exception> _exception;
        private readonly IErrorFactory _errorFactory;

        public DefaultOperationResult(IErrorFactory errorFactory)
        {
            _errorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
            _errors = new List<Error>();
            _exception = new List<Exception>();
        }

        public bool Succeeded => !HasError();

        public IEnumerable<Error> Errors => new ReadOnlyCollection<Error>(_errors);

        public IEnumerable<Exception> Exceptions => new ReadOnlyCollection<Exception>(_exception);

        public void AppendError(Error error)
        {
            if (error == null) { throw new ArgumentNullException(nameof(error)); }
            _errors.Add(error);
        }

        public void AppendException(Exception exception)
        {
            if (exception == null) { throw new ArgumentNullException(nameof(exception)); }
            _exception.Add(exception);
            var error = _errorFactory.Create(exception);
            AppendError(error);
        }

        public bool HasError()
        {
            return _errors.Count > 0;
        }

        public bool HasException()
        {
            return _exception.Count > 0;
        }
    }
}
