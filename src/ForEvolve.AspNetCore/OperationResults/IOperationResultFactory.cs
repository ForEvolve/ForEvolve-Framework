namespace ForEvolve.AspNetCore
{
    public interface IOperationResultFactory
    {
        IOperationResult Create();
        IOperationResult<TResult> Create<TResult>();
        IOperationResult<TResult> Create<TResult>(TResult result);
    }
}
