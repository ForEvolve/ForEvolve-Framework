namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents an <see cref="IOperationResult"/> standardizer.
    /// </summary>
    public interface IOperationResultStandardizer
    {
        /// <summary>
        /// Standardizes the specified operation result into a serializable object.
        /// </summary>
        /// <param name="operationResult">The operation result.</param>
        /// <returns>System.Object.</returns>
        object Standardize(IOperationResult operationResult);
    }
}
