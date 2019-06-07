using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForEvolve.OperationResults.MediatR
{
    /// <summary>
    /// This behavior runs all validators and returns the error as an 
    /// <see cref="IOperationResult"/> if validation fails.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<IOperationResult>
        where TResponse : IOperationResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Validate
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(r => r.Errors);
            var validationResult = new ValidationResult(failures);
            if (!validationResult.IsValid)
            {
                // Map errors to output
                var messages = validationResult.Errors
                    .Select(validationFailure => Map(validationFailure))
                    .ToArray();

                // Try to return the result
                var castedResult = OperationResult
                    .Failure(messages)
                    .ConvertTo<TResponse>();
                if (castedResult != null)
                {
                    return castedResult;
                }
            }
            return await next();
        }

        private static IMessage Map(ValidationFailure validationFailure)
        {
            var severity = Map(validationFailure.Severity);
            return new Message(severity, new
            {
                validationFailure.ErrorCode,
                validationFailure.ErrorMessage
            });
        }

        private static OperationMessageLevel Map(Severity severity)
        {
            switch (severity)
            {
                case Severity.Warning:
                    return OperationMessageLevel.Warning;
                case Severity.Info:
                    return OperationMessageLevel.Information;
                default:
                    return OperationMessageLevel.Error;
            }
        }
    }
}
