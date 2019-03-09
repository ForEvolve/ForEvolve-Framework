namespace ForEvolve.OperationResults.Standardizer
{
    /// <summary>
    /// Represents the <see cref="DefaultOperationResultStandardizer"/> options.
    /// </summary>
    public class DefaultOperationResultStandardizerOptions
    {
        /// <summary>
        /// The default member name of the operation values.
        /// </summary>
        public const string DefaultOperationName = "_operation";

        /// <summary>
        /// Gets or sets the member name of the operation values.
        /// </summary>
        /// <value>The member name of the operation values.</value>
        public string OperationName { get; set; } = DefaultOperationName;
    }
}
