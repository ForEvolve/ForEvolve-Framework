namespace ForEvolve.Api.Domain
{
    public interface IErrorFactory : IErrorFromExceptionFactory, IErrorFromDictionaryFactory, IErrorFromKeyValuePairFactory, IErrorFromRawValuesFactory
    {

        //Error Create<TException>(string errorCode, TException details)
        //    where TException : Exception;
        //Error Create<TException>(string errorCode, string errorMessage, TException details)
        //    where TException : Exception;
        //Error Create<TException>(string errorCode, string errorMessage, string errorTarget, TException details)
        //    where TException : Exception;

        //Error Create(string errorCode, Dictionary<string, object> details);
        //Error Create(string errorCode, string errorMessage, Dictionary<string, object> details);
        //Error Create(string errorCode, string errorMessage, string errorTarget, Dictionary<string, object> details);

        //Error Create(string errorCode, KeyValuePair<string, object> errorTargetAndMessage);

    }
}
