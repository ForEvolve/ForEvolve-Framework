using System;
using System.Collections.Generic;
using System.Text;
using ForEvolve.Api.Contracts.Errors;
using Microsoft.AspNetCore.Hosting;
using System.Collections;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromExceptionFactory : IErrorFromExceptionFactory
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IErrorFromRawValuesFactory _errorFromRawValuesFactory;
        public DefaultErrorFromExceptionFactory(IHostingEnvironment hostingEnvironment, IErrorFromRawValuesFactory errorFromRawValuesFactory)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _errorFromRawValuesFactory = errorFromRawValuesFactory ?? throw new ArgumentNullException(nameof(errorFromRawValuesFactory));
        }

        public Error Create<TException>(TException exception) where TException : Exception
        {
            var error = new Error
            {
                Code = CreateErrorCode(exception),
                Target = CreateErrorSource(exception),
                Message = exception.Message,
                InnerError = new InnerError
                {
                    HResult = exception.HResult.ToString(),
                }
            };
            if (_hostingEnvironment.IsDevelopment())
            {
                error.InnerError.Source = exception.Source;
                error.InnerError.StackTrace = exception.StackTrace;
                error.InnerError.HelpLink = exception.HelpLink;
                if(exception.Data != null && exception.Data.Count > 0)
                {
                    EnforceDetails(error);
                    var detailsCode = CreateDataErrorCode(error.Code);
                    foreach (DictionaryEntry data in exception.Data)
                    {
                        var subError = _errorFromRawValuesFactory.Create(
                            detailsCode,
                            data.Key.ToString(),
                            data.Value
                        );
                        error.Details.Add(subError);
                    }
                }
            }
            if(exception.InnerException != null)
            {
                EnforceDetails(error);
                var subError = Create(exception.InnerException);
                error.Details.Add(subError);
            }
            return error;
        }

        public void EnforceDetails(Error error)
        {
            if (error.Details == null)
            {
                error.Details = new List<Error>();
            }
        }

        public string CreateErrorSource<TException>(TException exception) where TException : Exception
        {
            return exception.TargetSite?.Name ?? exception.Source;
        }

        public string CreateErrorCode<TException>(TException exception)
        {
            return exception.GetType().Name;
        }

        public string CreateDataErrorCode(string errorCode)
        {
            return $"{errorCode}.Data";
        }
    }
}
