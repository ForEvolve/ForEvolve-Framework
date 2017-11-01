using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain
{
    public interface IOperationResult
    {
        bool Succeeded { get; }

        IEnumerable<Error> Errors { get; }
        IEnumerable<Exception> Exceptions { get; }

        void AppendError(Error error);
        void AppendException(Exception exception);

        bool HasException();
        bool HasError();
    }

    public interface IOperationResultFactory
    {
        IOperationResult Create();
    }

    public class DefaultOperationResultFactory : IOperationResultFactory
    {
        private readonly IErrorFactory _errorFactory;

        public DefaultOperationResultFactory(IErrorFactory errorFactory)
        {
            _errorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
        }

        public IOperationResult Create()
        {
            return new OperationResult(_errorFactory);
        }
    }

    public class OperationResult : IOperationResult
    {
        private readonly List<Error> _errors;
        private readonly List<Exception> _exception;
        private readonly IErrorFactory _errorFactory;

        public OperationResult(IErrorFactory errorFactory)
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
            _errors.Add(error);
        }

        public void AppendException(Exception exception)
        {
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
