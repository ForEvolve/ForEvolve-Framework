namespace ForEvolve.AspNetCore
{
    public class DefaultOperationResultWithResult<TResult> : DefaultOperationResult, IOperationResult<TResult>
    {
        public DefaultOperationResultWithResult(IErrorFactory errorFactory)
            : base(errorFactory)
        {
        }

        public TResult Result { get; set; }

        public bool HasResult()
        {
            return Result != null;
        }
    }
}
