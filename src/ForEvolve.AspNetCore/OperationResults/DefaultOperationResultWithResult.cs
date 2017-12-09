namespace ForEvolve.AspNetCore
{
    public class DefaultOperationResultWithResult<TResult> : DefaultOperationResult, IOperationResult<TResult>
    {
        public DefaultOperationResultWithResult(IErrorFactory errorFactory)
            : base(errorFactory)
        {
        }

        public TResult Value { get; set; }

        public bool HasResult()
        {
            return Value != null;
        }
    }
}
