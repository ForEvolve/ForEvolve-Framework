using System;
using System.Collections.Generic;
using System.Linq;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain.ErrorFactory.Implementations
{
    public class DefaultErrorFactory : IErrorFactory
    {
        private readonly IErrorFromExceptionFactory _errorFromExceptionFactory;
        private readonly IErrorFromDictionaryFactory _errorFromDictionaryFactory;
        private readonly IErrorFromKeyValuePairFactory _errorFromKeyValuePairFactory;
        private readonly IErrorFromRawValuesFactory _errorFromRawValuesFactory;

        public DefaultErrorFactory(
            IErrorFromExceptionFactory errorFromExceptionFactory,
            IErrorFromDictionaryFactory errorFromDictionaryFactory,
            IErrorFromKeyValuePairFactory errorFromKeyValuePairFactory,
            IErrorFromRawValuesFactory errorFromRawValuesFactory
        )
        {
            _errorFromExceptionFactory = errorFromExceptionFactory ?? throw new ArgumentNullException(nameof(errorFromExceptionFactory));
            _errorFromDictionaryFactory = errorFromDictionaryFactory ?? throw new ArgumentNullException(nameof(errorFromDictionaryFactory));
            _errorFromKeyValuePairFactory = errorFromKeyValuePairFactory ?? throw new ArgumentNullException(nameof(errorFromKeyValuePairFactory));
            _errorFromRawValuesFactory = errorFromRawValuesFactory ?? throw new ArgumentNullException(nameof(errorFromRawValuesFactory));
        }

        public Error Create<TException>(TException exception) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Error> Create(string errorCode, Dictionary<string, object> details)
        {
            throw new NotImplementedException();
        }

        public Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage)
        {
            throw new NotImplementedException();
        }

        public Error Create(string errorCode, string errorTarget, object errorMessage)
        {
            throw new NotImplementedException();
        }

        //public Error Create(string errorCode)
        //{
        //    return Create(errorCode, errorMessage: null);
        //}

        //public Error Create(string errorCode, string errorMessage)
        //{
        //    return Create(errorCode, errorMessage: errorMessage, errorTarget: null);
        //}

        //public Error Create(string errorCode, string errorMessage, string errorTarget)
        //{
        //    var error = new Error
        //    {
        //        Code = errorCode,
        //        Message = errorMessage,
        //        Target = errorTarget
        //    };
        //    return error;
        //}

        //public Error Create<TException>(TException exception) 
        //    where TException : Exception
        //{
        //    var error = Create(
        //        errorCode: exception.GetType().Name,
        //        errorMessage: exception.Message,
        //        errorTarget: exception.TargetSite?.Name
        //    );

        //    if (exception.InnerException != null)
        //    {
        //        error.Details = new List<Error>();
        //        var innerError = Create(exception.InnerException);
        //        error.Details.Add(innerError);
        //    }
        //    return error;
        //}

        //public Error Create<TException>(string errorCode, TException details)
        //    where TException : Exception
        //{
        //    return Create(errorCode, errorMessage: null, details: details);
        //}

        //public Error Create<TException>(string errorCode, string errorMessage, TException details)
        //    where TException : Exception
        //{
        //    return Create(errorCode, errorMessage: errorMessage, errorTarget: null, details: details);
        //}

        //public Error Create<TException>(string errorCode, string errorMessage, string errorTarget, TException details)
        //    where TException : Exception
        //{
        //    var error = Create(
        //        errorCode: errorCode,
        //        errorMessage: errorMessage,
        //        errorTarget: errorTarget
        //    );
        //    error.Details = new List<Error>
        //    {
        //        Create(details)
        //    };
        //    return error;
        //}

        ////public Error Create(string errorCode, KeyValuePair<string, object> detail)
        ////{
        ////    //return Create(errorCode, null, detail);
        ////    throw new NotImplementedException();
        ////}

        ////public Error Create(string errorCode, string errorMessage, string errorTarget, KeyValuePair<string, object> detail)
        ////{
        ////    throw new NotImplementedException();
        ////    //return Create(errorCode, null, null, detail);
        ////}

        ////public Error Create(string errorCode, string errorMessage, KeyValuePair<string, object> detail)
        ////{
        ////    throw new NotImplementedException();
        ////    //var error = new Error
        ////    //{
        ////    //    Code = errorCode,
        ////    //    Message = errorMessage,
        ////    //    Target = detail.Key
        ////    //};

        ////    //// TODO: unit test these scenarios
        ////    //switch (detail.Value)
        ////    //{
        ////    //    case string str:
        ////    //        error.Message = str;
        ////    //        break;
        ////    //    case IEnumerable<string> strCollection:
        ////    //        if (strCollection.Count() == 1)
        ////    //        {
        ////    //            error.Message = strCollection.First();
        ////    //        }
        ////    //        else
        ////    //        {
        ////    //            error.Details = strCollection
        ////    //                .Select(msg => new Error
        ////    //                {
        ////    //                    Code = CreateDetailsCode(errorCode),
        ////    //                    Message = msg
        ////    //                }).ToList();
        ////    //        }
        ////    //        break;
        ////    //    default:
        ////    //        error.Message = detail.ToString();
        ////    //        break;
        ////    //}

        ////    //return error;
        ////}
        //public Error Create(string errorCode, Dictionary<string, object> details)
        //{
        //    return Create(errorCode, null, details);
        //}
        //public Error Create(string errorCode, string errorMessage, Dictionary<string, object> details)
        //{
        //    return Create(errorCode, errorMessage, null, details);
        //}

        //public Error Create(string errorCode, string errorMessage, string errorTarget, Dictionary<string, object> details)
        //{
        //    var error = new Error
        //    {
        //        Code = errorCode,
        //        Message = errorMessage,
        //        Target = errorTarget,
        //        Details = details
        //            .Select(v => Create(CreateDetailsCode(errorCode), v)).ToList()
        //    };
        //    return error;
        //}

        //public Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //private string CreateDetailsCode(string errorCode)
        //{
        //    return $"{errorCode}Detail";
        //}
    }
}
