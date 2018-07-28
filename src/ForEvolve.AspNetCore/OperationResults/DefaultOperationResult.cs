using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;

namespace ForEvolve.AspNetCore
{
    public class DefaultOperationResult : IOperationResult
    {
        private readonly List<Error> _errors;
        private readonly List<Exception> _exceptions;
        private readonly IErrorFactory _errorFactory;

        public DefaultOperationResult(IErrorFactory errorFactory)
        {
            _errorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
            _errors = new List<Error>();
            _exceptions = new List<Exception>();
        }

        public bool Succeeded => !HasError();

        public IEnumerable<Error> Errors => new ReadOnlyCollection<Error>(_errors);

        public IEnumerable<Exception> Exceptions => new ReadOnlyCollection<Exception>(_exceptions);

        public void AddError(Error error)
        {
            if (error == null) { throw new ArgumentNullException(nameof(error)); }
            _errors.Add(error);
        }

        public void AddErrors(IEnumerable<Error> errors)
        {
            if (errors == null) { throw new ArgumentNullException(nameof(errors)); }
            foreach (var error in errors)
            {
                _errors.Add(error);
            }
        }

        public void AddErrorsFrom(IdentityResult result)
        {
            if (result == null) { throw new ArgumentNullException(nameof(result)); }
            foreach (var rawError in result.Errors)
            {
                var error = _errorFactory.Create(rawError);
                _errors.Add(error);
            }
        }

        public void AddErrorsFrom(IOperationResult result)
        {
            if (result == null) { throw new ArgumentNullException(nameof(result)); }
            _errors.AddRange(result.Errors);
            _exceptions.AddRange(result.Exceptions);
        }

        public void AddException(Exception exception)
        {
            if (exception == null) { throw new ArgumentNullException(nameof(exception)); }
            _exceptions.Add(exception);
            var error = _errorFactory.CreateFrom(exception);
            AddError(error);
        }

        public bool HasError()
        {
            return _errors.Count > 0;
        }

        public bool HasException()
        {
            return _exceptions.Count > 0;
        }
    }
}
