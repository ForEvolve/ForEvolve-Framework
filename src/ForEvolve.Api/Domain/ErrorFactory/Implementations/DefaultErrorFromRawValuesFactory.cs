using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain.ErrorFactory.Implementations
{
    public class DefaultErrorFromRawValuesFactory : IErrorFromRawValuesFactory
    {
        public Error Create(string errorCode, string errorTarget, object errorMessage)
        {
            var error = new Error(errorCode: errorCode, errorTarget: errorTarget);
            switch (errorMessage)
            {
                case string strErrorMessage:
                    error.Message = strErrorMessage;
                    break;
                case IEnumerable<string> strCollection:
                    if (strCollection.Count() == 1)
                    {
                        error.Message = strCollection.First();
                    }
                    else if(strCollection.Count() > 1)
                    {
                        error.Details = strCollection
                            .Select(msg => new Error
                            {
                                Code = $"{errorCode}Message",
                                Message = msg
                            }).ToList();
                    }
                    break;
                default:
                    error.Message = errorMessage.ToString();
                    break;
            }
            return error;
        }

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
    }
}
